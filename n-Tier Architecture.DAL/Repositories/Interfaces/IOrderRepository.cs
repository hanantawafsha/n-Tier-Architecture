using n_Tier_Architecture.DAL.DTO.Responses;
using NTierArchitecture.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.DAL.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetUserByOrderAsync(int orderId);
        Task<Order?> AddAsync(Order order);
        Task<List<OrderDto>> GetAllOrdersDtoAsync();
        Task<bool> ChangeStatusAsync(int orderId, StatusOrderEnum newStatus);
        Task<List<OrderDto>> GetAllOrderForUserAsync(string userId);
        Task<List<Order>> GetOrderByStatusAsync(StatusOrderEnum status);
        Task<bool> UserHasApprovderOrderforProductAsync(string userId, int productId);

    }
}
