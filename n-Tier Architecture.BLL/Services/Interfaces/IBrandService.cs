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
    public interface IBrandService: IGenericService<BrandRequest, BrandResponse, Brand>
    {
        Task<int> CreateFile(BrandRequest request);

    }
}
