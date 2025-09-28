using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class ProductRepository:GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task DescreaseQuantityAsync(List<(int productId, int quantity)> items)
        {
            var productIds = items.Select(i=>i.productId).ToList();
            var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();
            foreach (var product in products)
            {
                var item = items.First(i=>i.productId == product.Id);
                if(product.Quantity<item.quantity)
                {
                    throw new Exception("not enough ");
                }
                product.Quantity -= item.quantity;
            }
            await _context.SaveChangesAsync();
        }
        public async Task<List<Product>> GelAllProductsWithImageAsync()
        {
            return await _context.Products.Include(p=>p.SubImages).ToListAsync();
        }
    }
}
