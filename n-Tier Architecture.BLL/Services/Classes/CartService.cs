using NTierArchitecture.BLL.Services.Interfaces;
using NTierArchitecture.DAL.DTO.Requests;
using NTierArchitecture.DAL.DTO.Responses;
using NTierArchitecture.DAL.Models;
using NTierArchitecture.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.BLL.Services.Classes
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }


        public async Task<bool> AddToCartAsync(CartRequest request, string UserId)
        {
            var NewItem = new Cart
            {
                ProductId = request.ProductId,
                UserId  = UserId,
                Count = 1
            };
            return await _cartRepository.AddAsync(NewItem)>0;
        }

        public Task<bool> ClearCartAsync(string UserId)
        {
            return _cartRepository.ClearCartAsync(UserId);

        }

        public async Task<CartSummeryResponse> GetCartSummeryAsync(string UserId)
        {
            var cartItems =await _cartRepository.GetUserCartAsync(UserId);
            var response =  new CartSummeryResponse
            {
                Items = cartItems.Select(ci => new CartResponse
                {
                    ProductId=ci.ProductId,
                    ProductName = ci.Product.Name,
                    Price = ci.Product.Price,
                    Count = ci.Count
                } ).ToList()
            };
            return response;

        }

       
    }
}
