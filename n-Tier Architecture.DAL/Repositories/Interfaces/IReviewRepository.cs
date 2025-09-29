using n_Tier_Architecture.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n_Tier_Architecture.DAL.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task<bool> HasUserReviewProductAsync(string userId, int productId);
        Task AddReviewAsync(Review request, string userId);
    }
}
