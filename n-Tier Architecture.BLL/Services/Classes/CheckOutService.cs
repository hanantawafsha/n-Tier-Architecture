using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using NTierArchitecture.BLL.Services.Interfaces;
using NTierArchitecture.DAL.DTO.Requests;
using NTierArchitecture.DAL.DTO.Responses;
using NTierArchitecture.DAL.Models;
using NTierArchitecture.DAL.Repositories.Interfaces;
using Stripe.Checkout;

namespace NTierArchitecture.BLL.Services.Classes
{
    public class CheckOutService : ICheckOutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IEmailSender _emailSender;

        public CheckOutService(
            ICartRepository cartRepository,
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            IProductRepository productRepository,
            IEmailSender emailSender)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
            _emailSender = emailSender;
        }

        public async Task<CheckOutResponse> ProceedPaymentAsync(CheckOutRequest request, string UserId, HttpRequest httpRequest)
        {
            var cartItems = await _cartRepository.GetUserCartAsync(UserId);
            if (!cartItems.Any())
                return new CheckOutResponse { Success = false, Message = "Cart is Empty." };

            var order = new Order
            {
                UserId = UserId,
                PaymnetMethod = request.PaymentMethod,
                TotalPrice = cartItems.Sum(ci => ci.Product.Price * ci.Count),
                
            };

            await _orderRepository.AddAsync(order);
            //add order and decrease quantity 
            if (request.PaymentMethod == PaymnetMethodEnum.Cash)
            {
                order.StatusOrder = StatusOrderEnum.Approved;

                var orderItems = cartItems.Select(ci => new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = ci.ProductId,
                    Count = ci.Count,
                    Price = ci.Product.Price,
                    TotalPrice = ci.Count * ci.Product.Price
                }).ToList();

                await _orderItemRepository.AddRangeAsync(orderItems);

                var productUpdated = cartItems.Select(ci => (ci.ProductId, ci.Count)).ToList();
                await _productRepository.DescreaseQuantityAsync(productUpdated);

                return new CheckOutResponse
                {
                    Success = true,
                    Message = "Cash payment selected. Order placed successfully."
                };
            }

            // Visa
            if (request.PaymentMethod == PaymnetMethodEnum.Visa)
            {
                order.StatusOrder = StatusOrderEnum.Pending;

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    SuccessUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/Customer/CheckOuts/success/{order.Id}",
                    CancelUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/checkout/cancel"
                };

                foreach (var item in cartItems)
                {
                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name,
                                Description = item.Product.Description
                            },
                            UnitAmount = (long)(item.Product.Price * 100)
                        },
                        Quantity = item.Count
                    });
                }

                var service = new SessionService();
                var session = await service.CreateAsync(options);
                order.PaymentId = session.Id;

                return new CheckOutResponse
                {
                    Success = true,
                    Message = "Payment session created successfully.",
                    PaymentId = session.Id,
                    Url = session.Url
                };
            }

            return new CheckOutResponse { Success = false, Message = "Invalid payment method." };
        }

        public async Task<bool> SuccessPaymentAsync(int orderId)
        {
            var order = await _orderRepository.GetUserByOrderAsync(orderId);
            var subject = "";
            var body = "";

            if (order.PaymnetMethod == PaymnetMethodEnum.Visa)
            {
                order.StatusOrder = StatusOrderEnum.Approved;

                var cartItems = await _cartRepository.GetUserCartAsync(order.UserId);
                var orderItems = cartItems.Select(ci => new OrderItem
                {
                    OrderId = orderId,
                    ProductId = ci.ProductId,
                    Count = ci.Count,
                    Price = ci.Product.Price,
                    TotalPrice = ci.Count * ci.Product.Price
                }).ToList();

                await _orderItemRepository.AddRangeAsync(orderItems);

                var productUpdated = cartItems.Select(ci => (ci.ProductId, ci.Count)).ToList();
                await _productRepository.DescreaseQuantityAsync(productUpdated);

                subject = "Payment Successful - N-Tier Shop";
                body = $@"<h1>Thank you for your Payment</h1>
                          <p>Your payment for order {orderId}</p>
                          <p>Total Amount is : ${order.TotalPrice}</p>";
            }
            else if (order.PaymnetMethod == PaymnetMethodEnum.Cash)
            {
                subject = "Order placed successfully!";
                body = $@"<h1>Thank you for your order</h1>
                          <p>Your order {orderId} has been placed</p>
                          <p>Total Amount is : ${order.TotalPrice}</p>";
            }

            await _emailSender.SendEmailAsync(order.User.Email, subject, body);
            return true;
        }
    }
}
