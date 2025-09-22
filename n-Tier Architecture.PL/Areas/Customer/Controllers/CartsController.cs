using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using n_Tier_Architecture.BLL.Services.Interfaces;
using n_Tier_Architecture.DAL.DTO.Requests;
using System.Security.Claims;

namespace n_Tier_Architecture.PL.Areas.Customer.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService) {
            _cartService = cartService;
        }
        [HttpPost("")]
        public async Task<IActionResult> AddToCartAsync(CartRequest cartRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.AddToCartAsync(cartRequest, userId);
            return result ? Ok(new { message = "added to Cart Successfully" }) :BadRequest();
        }

        [HttpGet("")]
        public async Task<IActionResult> GetUserCartAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.GetCartSummeryAsync(userId);
            return Ok(result);

        }
    }
}
