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
    public class BrandService : GenericService<BrandRequest, BrandResponse, Brand>, IBrandService
    {
        public BrandService(IBrandRepository repository) :base(repository) { }
    }
}
