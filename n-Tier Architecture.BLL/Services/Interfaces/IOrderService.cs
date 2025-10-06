using n_Tier_Architecture.DAL.DTO.Responses;
using NTierArchitecture.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order?> GetUserByOrderAsync(int orderId);
        Task<Order?> AddAsync(Order order);
        Task<List<OrderDto>> GetAllOrderForUserAsync(string userId);
        Task<List<OrderDto>> GetAllOrderDetailsAsync();
        Task<bool> ChangeStatusAsync(int orderId,StatusOrderEnum newStatus);
        Task<List<Order>> GetOrderByStatusAsync(StatusOrderEnum status);
    }
}
