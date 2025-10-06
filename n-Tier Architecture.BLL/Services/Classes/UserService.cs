using Mapster;
using Microsoft.AspNetCore.Identity;
using NTierArchitecture.BLL.Services.Interfaces;
using NTierArchitecture.DAL.DTO.Responses;
using NTierArchitecture.DAL.Models;
using NTierArchitecture.DAL.Repositories.Interfaces;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.BLL.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IUserRepository userRepository,UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                var role = await _userManager.GetRolesAsync(user);
                userDtos.Add(new UserDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    RoleName = role.FirstOrDefault()
                });
            }
            return userDtos;
        }

        public async Task<UserDto> GetByIdAsync(string userId)
        {
            var user =  await _userRepository.GetByIdAsync(userId);
            var userDto = user.Adapt<UserDto>();
            //get current role
            var role = await _userManager.GetRolesAsync(user);
            userDto.RoleName = role.FirstOrDefault();
            return userDto;
        }

        public async Task<bool> BlockUserAsync(string userId, int days)
        {
            var result = await _userRepository.BlockUserAsync(userId, days);
            return result;
        }

        public async Task<bool> UnBlockUserAsync(string userId)
        {
            return await _userRepository.UnBlockUserAsync(userId);
        }
        public async Task<bool> IsBlockAsync(string userId)
        {
            return await _userRepository.IsBlockAsync(userId);
        }
        public async Task<bool>ChangeUserRoleAsync(string userId, string roleName)
        {
            return await _userRepository.ChangeUserRole(userId, roleName);
        }
    }
}
