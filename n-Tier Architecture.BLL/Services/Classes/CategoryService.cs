using Azure;
using Azure.Core;
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
    public class CategoryService : GenericService<CategoryRequest, CategoryResponse, Category>, ICategoryService
    {
        
        public CategoryService(ICategoryRepository repository): base(repository){ 
        }
    }
}