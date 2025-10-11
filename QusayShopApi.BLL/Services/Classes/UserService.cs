using Mapster;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.DTO.Responses;
using QusayShopApi.DAL.Models;
using QusayShopApi.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.BLL.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;

        public UserService(IUserRepository _userRepository, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager)
        {
            this._userRepository = _userRepository;
            _userManager = userManager;
        }

       

        public async Task<List<UserDTO>> GettAllUserAsync()
        {
            var users= await _userRepository.GetAllUserAsync();
            var UserDTOS = new List<UserDTO>();
            foreach (var user in users) {
                var roles = await _userManager.GetRolesAsync(user);
                UserDTOS.Add( new UserDTO {
                    Id= user.Id,
                    FullName= user.FullName,
                    Email= user.Email,
                    PhoneNumber= user.PhoneNumber,
                    UserName= user.UserName,
                    emailConfirmed= user.EmailConfirmed,
                    UserRole= roles.FirstOrDefault() ?? "No Role"

                });
            }
            return UserDTOS;
        }

        public async Task<UserDTO> GetUserByIdAsync(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return user.Adapt<UserDTO>();
        }
        public Task<string> BlockUserAsync(string id, int days)
        {
            return _userRepository.BlockUserAsync(id, days);
        }
        public Task<string> IsBlockedUserAsync(string userId)
        {
            return _userRepository.IsBlockedUserAsync(userId);
        }

        public Task<string> UnBlockUserAsync(string userId)
        {
            return _userRepository.UnBlockUserAsync(userId);
        }
        public Task<string> ChangeUserRole(string userId, userDTOChangeRole Role) { 
            return _userRepository.ChangeUserRole(userId, Role.NewRole);
        }
    }
}
