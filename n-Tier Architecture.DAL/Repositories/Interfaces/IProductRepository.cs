using NTierArchitecture.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.DAL.Repositories.Interfaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task DescreaseQuantityAsync(List<(int productId, int quantity)>items);
        Task<List<Product>> GelAllProductsWithImageAsync();
    }
}
