using NTierArchitecture.DAL.DTO.Responses;
using NTierArchitecture.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();
        Task <UserDto> GetByIdAsync(string userId);
        Task<bool> BlockUserAsync(string userId, int days);
        Task<bool> UnBlockUserAsync(string userId);
        Task<bool> IsBlockAsync(string userId);
        Task<bool> ChangeUserRoleAsync(string userId, string roleName);

    }
}
