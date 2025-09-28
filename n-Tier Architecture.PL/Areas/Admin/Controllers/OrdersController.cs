using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using n_Tier_Architecture.BLL.Services.Interfaces;
using n_Tier_Architecture.DAL.Models;

namespace n_Tier_Architecture.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetOrderByStatus(StatusOrderEnum status)
        {
            var orders = await _orderService.GetOrderByStatusAsync(status);
            return Ok(orders);
        }

        [HttpPatch("change-status/{orderId}")]
        public async Task<IActionResult> ChangeOrderStatus (int orderId, [FromBody] StatusOrderEnum newStatus)
        {
            var result = await _orderService.ChangeStatusAsync(orderId, newStatus);
            return Ok(result);
        }
    }
}
