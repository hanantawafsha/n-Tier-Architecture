using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using n_Tier_Architecture.DAL.DTO.Requests;
using n_Tier_Architecture.DAL.DTO.Responses;
using n_Tier_Architecture.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n_Tier_Architecture.BLL.Services.Interfaces
{
    public interface IProductService : IGenericService<ProductRequest, ProductResponse, Product>
    {
       Task<int> CreateProduct(ProductRequest request);
        Task<List<ProductResponse>> GelAllProductsAsync(HttpRequest request, bool onlyActive = false);

    }
}
