using Mapster;
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

        public UserService(IUserRepository _userRepository)
        {
            this._userRepository = _userRepository;
        }

       

        public async Task<List<UserDTO>> GettAllUserAsync()
        {
            var users= await _userRepository.GetAllUserAsync();
            return users.Adapt<List<UserDTO>>();
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

        public Task<string> UnBlockUserAsymc(string iduserId)
        {
            return _userRepository.UnBlockUserAsymc(iduserId);
        }
    }
}
