using Microsoft.EntityFrameworkCore;
using n_Tier_Architecture.DAL.Data;
using n_Tier_Architecture.DAL.Models;
using n_Tier_Architecture.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n_Tier_Architecture.DAL.Repositories.Classes
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
        public async Task<List<Order>> GetAllOrderForUserAsync(string userId)
        {
            return await _context.Orders.Include(o=>o.UserId).ToListAsync();  
            //where
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



    }
}
