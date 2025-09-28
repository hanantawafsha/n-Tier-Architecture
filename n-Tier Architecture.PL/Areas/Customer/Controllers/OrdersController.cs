using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using n_Tier_Architecture.BLL.Services.Interfaces;

namespace n_Tier_Architecture.PL.Areas.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{userId}")]
        //get order for specific user.
        public async Task<IActionResult> GetAll([FromRoute] string userId)
        {
            var result = await _orderService.GetAllOrderForUserAsync(userId);
            return Ok(result);
        }
    }
}
