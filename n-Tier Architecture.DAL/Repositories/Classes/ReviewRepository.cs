using Microsoft.EntityFrameworkCore;
using NTierArchitecture.DAL.Data;
using NTierArchitecture.DAL.Models;
using NTierArchitecture.DAL.Repositories.Interfaces;


namespace NTierArchitecture.DAL.Repositories.Classes
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> HasUserReviewProductAsync(string userId, int productId)
        {
            return await _context.Reviews.AnyAsync(r=>r.UserId == userId && r.ProductId == productId);

        }
        public async Task AddReviewAsync(Review request, string userId)
        {
            request.UserId = userId;
            request.CreatedAt = DateTime.UtcNow; 
            await _context.Reviews.AddAsync(request);
            await _context.SaveChangesAsync();            
        }
    }
}
            
