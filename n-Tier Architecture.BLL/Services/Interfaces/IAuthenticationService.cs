using Microsoft.AspNetCore.Http;
using NTierArchitecture.DAL.DTO.Requests;
using NTierArchitecture.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.BLL.Services.Interfaces
{
    public interface IAuthenticationService
    {
       Task<UserResponse> LoginAsync(LoginRequest loginRequest);
        Task<UserResponse> RegisterAsync(RegisterRequest registerRequest, HttpRequest httpRequest);
        Task<string> ConfirmEmail(string token, string userId);
        Task<bool> ForgotPassword(ForgotPasswordRequest request);
        Task<bool> ResetPassword(ResetPasswordRequest request);

    }
}
