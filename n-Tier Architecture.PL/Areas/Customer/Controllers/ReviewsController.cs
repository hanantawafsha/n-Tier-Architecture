using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTierArchitecture.BLL.Services.Interfaces;
using NTierArchitecture.DAL.DTO.Requests;
using System.Security.Claims;

namespace NTierArchitecture.PL.Areas.Customer.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles ="Customer")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddReview([FromBody] ReviewRequest reviewRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _reviewService.AddReviewAsync(reviewRequest, userId);

            return Ok(result);
            
        }
    }
}
