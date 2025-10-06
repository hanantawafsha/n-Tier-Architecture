using Mapster;
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
    public class BrandService : GenericService<BrandRequest, BrandResponse, Brand>, IBrandService
    {
        private readonly IBrandRepository _repository;
        private readonly IFileService _fileService;

        public BrandService(IBrandRepository repository, IFileService fileService) :base(repository)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<int> CreateFile(BrandRequest request)
        {
            var brand = request.Adapt<Brand>();
            brand.Name = request.Name;

            if (request.MainImage != null)
            {
                var filePath = await _fileService.UploadAsync(request.MainImage);
                brand.MainImage = filePath;
            }
            return _repository.Add(brand);
        }
        
    }
}
