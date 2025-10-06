using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTierArchitecture.BLL.Services.Interfaces;
using NTierArchitecture.DAL.DTO.Responses;
using NTierArchitecture.DAL.DTO.Requests;




namespace NTierArchitecture.PL.Areas.Identity.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Identity")]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountsController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserResponse>> Register (RegisterRequest registerRequest)
        {
            var result = await _authenticationService.RegisterAsync(registerRequest,Request);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> Login(LoginRequest loginRequest)
        {
            var result = await _authenticationService.LoginAsync(loginRequest);
            return Ok(result);
        }

        [HttpGet("confirmEmail")]
        public async Task<ActionResult<string>> ConfirmEmail([FromQuery] string token, [FromQuery] string userId)
        {
            var result = await _authenticationService.ConfirmEmail(token, userId);
            return Ok(result);
        }

        [HttpPost("forgotPassword")]
        public async Task<ActionResult<string>> ForgotPassowrd([FromBody] ForgotPasswordRequest request)
        {
            var result = await _authenticationService.ForgotPassword(request);
            return Ok(result);
        }

        [HttpPatch("resetPassword")]
        public async Task<ActionResult<string>> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var result = await _authenticationService.ResetPassword(request);
            return Ok(result);

        }

    }
}
