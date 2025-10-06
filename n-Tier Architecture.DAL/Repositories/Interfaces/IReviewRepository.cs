using NTierArchitecture.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.DAL.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task<bool> HasUserReviewProductAsync(string userId, int productId);
        Task AddReviewAsync(Review request, string userId);
    }
}
