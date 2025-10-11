using QusayShopApi.DAL.DTO.Responses;
using QusayShopApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.BLL.Services.Interfaces
{
    public  interface IUserService
    {
        Task<List<UserDTO>> GettAllUserAsync();
        Task<UserDTO> GetUserByIdAsync(string id);
        Task<string> BlockUserAsync(string id, int days);
        Task<string> UnBlockUserAsync(string userId);
        Task<string> IsBlockedUserAsync(string userId);
        Task<string> ChangeUserRole(string userId, userDTOChangeRole Role);
    }
}
