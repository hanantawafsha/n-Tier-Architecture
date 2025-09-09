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
        public int Add(Cart cart)
        {
            _context.Carts.Add(cart);
        }
    }
}
