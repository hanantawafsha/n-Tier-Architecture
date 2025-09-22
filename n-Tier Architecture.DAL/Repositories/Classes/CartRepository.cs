using Microsoft.EntityFrameworkCore;
using n_Tier_Architecture.DAL.Data;
using n_Tier_Architecture.DAL.Models;
using n_Tier_Architecture.DAL.Repositories.Interfaces;

namespace n_Tier_Architecture.BLL.Services.Classes
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext Context)
        {
            _context = Context;
        }
        public async Task<int> AddAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> ClearCartAsync(string UserId)
        {
            var items = _context.Carts.Where(c => c.UserId == UserId).ToList();

            _context.Carts.RemoveRange(items);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Cart>> GetUserCartAsync(string UserId)
        {
            return await _context.Carts.Include(c=>c.Product).Where(c=>c.UserId == UserId).ToListAsync();
        }
    }
}
