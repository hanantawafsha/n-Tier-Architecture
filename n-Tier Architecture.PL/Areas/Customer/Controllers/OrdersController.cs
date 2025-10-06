using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using n_Tier_Architecture.DAL.DTO.Responses;
using NTierArchitecture.BLL.Services.Interfaces;

namespace NTierArchitecture.PL.Areas.Customer.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{userId}")]
        //get order for specific user.
        public async Task<ActionResult<List<OrderDto>>> GetAll([FromRoute] string userId)
        {
            var result = await _orderService.GetAllOrderForUserAsync(userId);
            return Ok(result);
        }
       // [HttpPost("")]
        //public async Task<IActionResult> AddAsync()
        //{

        //}
    }
}
