using NTierArchitecture.DAL.DTO.Requests;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.BLL.Services.Interfaces
{
    public interface IReviewService
    {
        Task<bool> AddReviewAsync(ReviewRequest request, String userId);
   
    }
}
