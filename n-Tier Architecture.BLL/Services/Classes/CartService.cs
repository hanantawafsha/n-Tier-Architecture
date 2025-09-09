using n_Tier_Architecture.BLL.Services.Interfaces;
using n_Tier_Architecture.DAL.DTO.Requests;
using n_Tier_Architecture.DAL.Models;
using n_Tier_Architecture.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n_Tier_Architecture.BLL.Services.Classes
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public bool AddToCart(CartRequest request, string UserId)
        {
            var NewItem = new Cart
            {
                ProductId = request.ProductId,
                UserId  = UserId,
                Count = 1
            };
            return _cartRepository.Add(NewItem)>0;
        }
    }
}
