using n_Tier_Architecture.DAL.DTO.Responses;
using NTierArchitecture.BLL.Services.Interfaces;
using NTierArchitecture.DAL.Models;
using NTierArchitecture.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.BLL.Services.Classes
{
    public class OrderService:IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order?> AddAsync(Order order)
        {
            return await _orderRepository.AddAsync(order);
        }

        public async Task<List<OrderDto>> GetAllOrderDetailsAsync()
        {

            return await _orderRepository.GetAllOrdersDtoAsync();
        }

        public async Task<bool> ChangeStatusAsync(int orderId, StatusOrderEnum newStatus)
        {
            return await _orderRepository.ChangeStatusAsync(orderId, newStatus);
        }

        public async Task<List<OrderDto>> GetAllOrderForUserAsync(string userId)
        {
            return await _orderRepository.GetAllOrderForUserAsync(userId);
        }

        public async Task<List<Order>> GetOrderByStatusAsync(StatusOrderEnum status)
        {
            return await _orderRepository.GetOrderByStatusAsync(status);
        }

        public async Task<Order?> GetUserByOrderAsync(int orderId)
        {
            return await _orderRepository.GetUserByOrderAsync(orderId);
        }

    }
}