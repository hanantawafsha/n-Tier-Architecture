using Azure;
using Azure.Core;
using Mapster;
using n_Tier_Architecture.BLL.Services.Interfaces;
using n_Tier_Architecture.DAL.DTO.Requests;
using n_Tier_Architecture.DAL.DTO.Responses;
using n_Tier_Architecture.DAL.Models;
using n_Tier_Architecture.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n_Tier_Architecture.BLL.Services.Classes
{
    public class ProductService : GenericService<ProductRequest, ProductResponse, Product>, IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IFileService _fileService;

        public ProductService(IProductRepository repository, IFileService fileService
            ): base(repository){
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<int> CreateFile(ProductRequest request)
        {
            var product = request.Adapt<Product>();
            product.Name = request.Name;
            product.Description = request.Description;

            if (request.MainImage != null)
            {
               var filePath = await _fileService.UploadAsync(request.MainImage);
                product.MainImage =  filePath;
            }
            return _repository.Add(product);
        }
        

    }
}