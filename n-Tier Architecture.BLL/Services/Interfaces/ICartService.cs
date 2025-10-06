using NTierArchitecture.DAL.DTO.Requests;
using NTierArchitecture.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.BLL.Services.Interfaces
{
    public interface ICartService
    {
        Task<bool> AddToCartAsync(CartRequest request, string UserId);
        Task<CartSummeryResponse> GetCartSummeryAsync(string UserId);
        Task<bool> ClearCartAsync(string UserId);
    }
}
