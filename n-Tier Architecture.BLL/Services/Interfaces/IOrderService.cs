using n_Tier_Architecture.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n_Tier_Architecture.BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order?> GetUserByOrderAsync(int orderId);
        Task<Order?> AddAsync(Order order);
        Task<bool> ChangeStatusAsync(int orderId,StatusOrderEnum newStatus);
        Task<List<Order>> GetAllOrderForUserAsync(string userId);
        Task<List<Order>> GetOrderByStatusAsync(StatusOrderEnum status);
    }
}
