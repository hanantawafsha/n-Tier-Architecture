using Mapster;
using NTierArchitecture.BLL.Services.Interfaces;
using NTierArchitecture.DAL.DTO.Requests;
using NTierArchitecture.DAL.Models;
using NTierArchitecture.DAL.Repositories.Classes;
using NTierArchitecture.DAL.Repositories.Interfaces;


namespace NTierArchitecture.BLL.Services.Classes
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

