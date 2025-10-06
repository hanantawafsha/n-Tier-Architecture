using Microsoft.EntityFrameworkCore;
using n_Tier_Architecture.DAL.DTO.Responses;
using NTierArchitecture.DAL.Data;
using NTierArchitecture.DAL.Models;
using NTierArchitecture.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.DAL.Repositories.Classes
{
    public class OrderRepository:IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> AddAsync(Order order)
        {
             await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> GetUserByOrderAsync(int orderId)
        {

            return await _context.Orders.Include(o => o.User).FirstOrDefaultAsync(o=> o.Id == orderId);
        }
       
        public async Task<List<OrderDto>> GetAllOrdersDtoAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .ToListAsync();

            var ordersDto =  orders.Select(o => new OrderDto
            {
                Id = o.Id,
                StatusOrder = o.StatusOrder,
                TotalPrice = o.TotalPrice,
                ShippedDate = o.ShippedDate,
                UserFullName = o.User.FullName,
                Items = o.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    Price = oi.Price,
                    Count = oi.Count,
                    TotalPrice = oi.TotalPrice
                }).ToList()
            }).ToList();

            return ordersDto;
        
        }
        public async Task<List<OrderDto>> GetAllOrderForUserAsync(string userId)
        {
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.User)
                .ToListAsync();

            var ordersDto = orders.Select(o => new OrderDto
            {
                Id = o.Id,
                StatusOrder = o.StatusOrder,
                TotalPrice = o.TotalPrice,
                ShippedDate = o.ShippedDate,
                UserFullName = o.User.FullName,
                Items = o.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    Price = oi.Price,
                    Count = oi.Count,
                    TotalPrice = oi.TotalPrice
                }).ToList()
            }).ToList();

            return ordersDto;
        }

        public async Task<List<Order>> GetOrderByStatusAsync(StatusOrderEnum status)
        {
            return await _context.Orders.Where(o =>o.StatusOrder == status)
                .OrderByDescending(o=>o.ShippedDate).ToListAsync();

        }
        public async Task<bool> ChangeStatusAsync(int orderId, StatusOrderEnum newStatus)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return false;
            }
            order.StatusOrder = newStatus;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UserHasApprovderOrderforProductAsync(string userId, int productId)
        {
            return await _context.Orders.Include(o => o.OrderItems)
                .AnyAsync(e => e.UserId == userId 
            && e.StatusOrder == StatusOrderEnum.Approved 
            && e.OrderItems.Any(oi => oi.ProductId == productId));
        }



    }
}
