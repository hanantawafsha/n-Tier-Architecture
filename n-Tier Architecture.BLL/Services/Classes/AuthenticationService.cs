using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NTierArchitecture.BLL.Services.Interfaces;
using NTierArchitecture.DAL.DTO.Requests;
using NTierArchitecture.DAL.DTO.Responses;
using NTierArchitecture.DAL.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.BLL.Services.Classes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _singInManager;

        public AuthenticationService( UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IEmailSender emailSender,
            SignInManager<ApplicationUser> singInManager) 
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
            _singInManager = singInManager;
        }
        public async Task<UserResponse> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null)
            {
                throw new Exception("Invalid email or password");
            }

            var result = await _singInManager.CheckPasswordSignInAsync(user, loginRequest.Password,true);
            if (result.Succeeded)
            {
                return new UserResponse
                {
                    Token = await CreateTokenAsync(user)
                };
            }
            else if (result.IsLockedOut)
            {
                throw new Exception("your account is locked");
            }
            //confirmed email
            else if (result.IsNotAllowed)
            {
                throw new Exception("please confirm your email");
            }
            else
            {
                throw new Exception("Invalid email or password");

            }
        }

        public async Task<string> ConfirmEmail(string token, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                throw new Exception("user not found");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return " email confirmed successfully";
            }

            return "email confirmation failed";
        }
        public async Task<UserResponse> RegisterAsync(RegisterRequest reqisterRequest, HttpRequest httpRequest)
        {
            var user = new ApplicationUser()
            {
                FullName = reqisterRequest.FullName,
                Email = reqisterRequest.Email,
                PhoneNumber = reqisterRequest.PhoneNumber,
                UserName = reqisterRequest.UserName
            };
            var result = await _userManager.CreateAsync(user,reqisterRequest.Password);
            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var escapeToken = Uri.EscapeDataString(token);
                var emailurl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/identity/Accounts/ConfirmEmail?token={escapeToken}&userid={user.Id}";
                await _emailSender.SendEmailAsync(user.Email, "NTier Shop - Confrim your email",$"<h1> Hello {user.UserName}</h1>" +
                    $"<a href='{emailurl}'> confirm </a>");

                //add customer role to the created users
                await _userManager.AddToRoleAsync(user, "Customer");
                return new UserResponse()
                {
                    Token = reqisterRequest.Email
                };
            }
            else
            {
                throw new Exception($"{result.Errors}");
            }
        }

        private async Task<string> CreateTokenAsync (ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles) 
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
             }
           // var secretKey = "ojbakcNgKARgp6V0lZulePr24crglUfz";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("jwtOptions")["SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims:Claims,
                expires: DateTime.Now.AddDays(15),
                signingCredentials:credentials

                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<bool> ForgotPassword ( ForgotPasswordRequest request )
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                throw new Exception("User not found");
            }
            var random = new Random();
            var code = random.Next(1000,9999).ToString();
            user.CodeResetPassword = code;
            user.PasswordResetCodeExpire = DateTime.UtcNow.AddMinutes(15);
        
            await _userManager.UpdateAsync(user);
            await _emailSender.SendEmailAsync(request.Email, "Reset Password", $"<p> your code to reset your password is {code}");
            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                throw new Exception("User not found");
            }

            if (request.Code != user.CodeResetPassword)
            {
                return false;
            }
            if (user.PasswordResetCodeExpire < DateTime.UtcNow) return false;
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
            if (result.Succeeded)
            {
                await _emailSender.SendEmailAsync(request.Email, "Change Password", "<h1> your password has been changed</h1>");
            }
                return true;
        }


    }
}
