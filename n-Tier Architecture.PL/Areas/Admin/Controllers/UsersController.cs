using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using n_Tier_Architecture.BLL.Services.Interfaces;
using n_Tier_Architecture.DAL.DTO.Requests;

namespace n_Tier_Architecture.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }
        [HttpPatch("block/{userId}")]
        public async Task<IActionResult> BlockUser([FromRoute] string userId, [FromBody] int days)
        {
            var result = await _userService.BlockUserAsync(userId, days);
            return Ok(result);
        }
        [HttpPatch("unblock/{userId}")]

        public async Task<IActionResult> UnBlockUser([FromRoute] string userId)
        {
            var result = await _userService.UnBlockUserAsync(userId);
            return Ok(result);
        }
        [HttpGet("isblock/{userId}")]
        public async Task<IActionResult> IsBlock([FromRoute] string userId)
        {
            var result = await _userService.IsBlockAsync(userId);
            return Ok(result);

        }

        [HttpPatch("changerole/{userId}")]
        public async Task<IActionResult> ChangeUserRole([FromRoute]string userId, [FromBody] ChangeRoleRequest request)
        {
            var result= await _userService.ChangeUserRoleAsync(userId, request.RoleName);
            return Ok(new {message = "role changed successfully!"});
        }
    }
}
