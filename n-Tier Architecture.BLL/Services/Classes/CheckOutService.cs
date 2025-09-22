using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using n_Tier_Architecture.BLL.Services.Interfaces;
using n_Tier_Architecture.DAL.DTO.Requests;
using n_Tier_Architecture.DAL.DTO.Responses;
using n_Tier_Architecture.DAL.Models;
using n_Tier_Architecture.DAL.Repositories.Classes;
using n_Tier_Architecture.DAL.Repositories.Interfaces;
using Stripe;
using Stripe.Checkout;


namespace n_Tier_Architecture.BLL.Services.Classes
{
    public class CheckOutService : ICheckOutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IEmailSender _emailSender;

        public CheckOutService(ICartRepository cartRepository,
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
            if(!cartItems.Any())
            {
                return new CheckOutResponse
                {
                    Success = false,
                    Message = "Cart is Empty."
                };
            }
            Order order = new Order
            {
                UserId = UserId,
                PaymnetMethod = request.PaymentMethod,
                TotalPrice = cartItems.Sum(ci=>ci.Product.Price * ci.Count)
                
                //coupon and discount
            };

             await _orderRepository.AddAsync(order);

            if (request.PaymentMethod == PaymnetMethodEnum.Cash)
            {
                return new CheckOutResponse
                {
                    Success = true,
                    Message = "Cash."
                };

            }
            if (request.PaymentMethod == PaymnetMethodEnum.Visa)
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
            {
               
            },
                    Mode = "payment",
                    SuccessUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/Customer/CheckOuts/success/{order.Id}",
                    CancelUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/checkout/cancel",
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
                                Description = item.Product.Description,
                            },
                            UnitAmount = (long)(item.Product.Price * 100),
                        },
                        Quantity = item.Count,
                    });
                }
                var service = new SessionService();
                var session = await service.CreateAsync(options);
               order.PaymentId=session.Id;
                return new CheckOutResponse
                {
                    Message = "Payment session created successfully. ",
                    Success = true,
                    PaymentId= session.Id,
                    Url = session.Url

                };


            }
            return new CheckOutResponse
            {
                Success = false,
                Message = "Invalid payment method."
            };

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
                var orderItems = new List<OrderItem>();
                var productUpdated = new List<(int productId, int quantity)>();
                foreach(var cartItem in cartItems)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = orderId,
                        ProductId = cartItem.ProductId,
                        TotalPrice = cartItem.Product.Price * cartItem.Count,
                        Count = cartItem.Count,
                        Price = cartItem.Product.Price,
                    };
                    orderItems.Add(orderItem);
                    productUpdated.Add((cartItem.ProductId,cartItem.Count));


                    // to be continued 

                }
                await _orderItemRepository.AddRangeAsync(orderItems);
                await _cartRepository.ClearCartAsync(order.UserId);
                await _productRepository.DescreaseQuantityAsync(productUpdated);


                subject = "Payment Successful - N-Tier Shop";
                body = $@"<h1>Thank you for your Payment</h1>
                    <p>Your payment for order {orderId}</p>
                     <p> total Amount is :${order.TotalPrice} </p>";

            }
            else if (order.PaymnetMethod == PaymnetMethodEnum.Cash)
            {
                subject = "order placed successfully!";
                body = $@"<h1>Thank you for your order</h1>
                    <p>Your payment for order {orderId}</p>
                     <p> total Amount is :${order.TotalPrice} </p>";
            }
            await _emailSender.SendEmailAsync(order.User.Email, subject, body);
            return true; 
        }
    }
}
