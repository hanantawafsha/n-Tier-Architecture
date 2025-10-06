using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using NTierArchitecture.DAL.DTO.Requests;
using NTierArchitecture.DAL.DTO.Responses;
using NTierArchitecture.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.BLL.Services.Interfaces
{
    public interface IProductService : IGenericService<ProductRequest, ProductResponse, Product>
    {
       Task<int> CreateProduct(ProductRequest request);
        Task<List<ProductResponse>> GelAllProductsAsync(HttpRequest request, bool onlyActive = false, int pageNumber = 1, int pageSize = 1);

    }
}
