using Mapster;
using n_Tier_Architecture.BLL.Services.Interfaces;
using n_Tier_Architecture.DAL.DTO.Requests;
using n_Tier_Architecture.DAL.Models;
using n_Tier_Architecture.DAL.Repositories.Interfaces;


namespace n_Tier_Architecture.BLL.Services.Classes
{
    public class ReviewService : IReviewService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IOrderRepository orderRepository,
            IReviewRepository reviewRepository)
        {
            _orderRepository = orderRepository;
            _reviewRepository = reviewRepository;
        }
        public async Task<bool> AddReviewAsync(ReviewRequest request, string userId)
        {
           var hasOrder = await _orderRepository.UserHasApprovderOrderforProductAsync(userId, request.ProductId);
            if (!hasOrder) return false;
            var alreadyReviewed = await _reviewRepository.HasUserReviewProductAsync(userId, request.ProductId);
            if (alreadyReviewed) return false;
            var review = request.Adapt<Review>();

            await _reviewRepository.AddReviewAsync(review,userId);
            return true;
        }
    }
}
