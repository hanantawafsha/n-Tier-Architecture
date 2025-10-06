using Azure;
using Azure.Core;
using Mapster;
using Microsoft.AspNetCore.Http;
using NTierArchitecture.BLL.Services.Interfaces;
using NTierArchitecture.DAL.DTO.Requests;
using NTierArchitecture.DAL.DTO.Responses;
using NTierArchitecture.DAL.Models;
using NTierArchitecture.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.BLL.Services.Classes
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

        public async Task<int> CreateProduct(ProductRequest request)
        {
            var product = request.Adapt<Product>();
            product.Name = request.Name;
            product.Description = request.Description;

            if (request.MainImage != null)
            {
               var filePath = await _fileService.UploadAsync(request.MainImage);
                product.MainImage =  filePath;
            }
            if(request.SubImages !=null)
            {
                var subImagesPath = await _fileService.UploadManyAsync(request.SubImages);
               //question
                product.SubImages = subImagesPath.Select(img => new ProductImage
                {
                    ImageName = img
                }).ToList();
            }
            return _repository.Add(product);
        }
        
        public async Task<List<ProductResponse>> GelAllProductsAsync(HttpRequest request,bool onlyActive=false,int pageNumber =1, int pageSize =1)
        {
            var products = await _repository.GelAllProductsWithImageAsync();

            if(onlyActive)
            {
                products=products.Where(p=>p.Status==Status.Active).ToList();
            }
            //pagination 
            var pageProducts = products.Skip((pageNumber  - 1) * pageSize).Take(pageSize);
            return pageProducts.Select(p => new ProductResponse
            {
                Name = p.Name,
                Quantity = p.Quantity,
                MainImageUrl = $"{request.Scheme}://{request.Host}/Images/{p.MainImage}",
                SubImagesUrl = p.SubImages.Select(img => $"{request.Scheme}://{request.Host}/Images/{img.ImageName}").ToList(),
                Reviews = p.Reviews.Select(r=> new ReviewResponse
                {
                    Id = r.Id,
                    FullName=r.User.FullName,
                    Comment = r.Comment,
                    Rate = r.Rate,

                }).ToList()
            }).ToList();
        }

    }
}