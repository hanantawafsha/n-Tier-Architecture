using Microsoft.EntityFrameworkCore;
using n_Tier_Architecture.DAL.Data;
using n_Tier_Architecture.DAL.Models;
using n_Tier_Architecture.DAL.Repositories.Interfaces;


namespace n_Tier_Architecture.DAL.Repositories.Classes
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
