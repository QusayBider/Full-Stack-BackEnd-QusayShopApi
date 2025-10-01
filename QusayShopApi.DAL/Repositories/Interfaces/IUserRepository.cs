using QusayShopApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetAllUserAsync();
        Task<ApplicationUser?> GetUserByIdAsync(string id);
        Task<string> BlockUserAsync(string id, int days);
        Task<string> UnBlockUserAsymc(string iduserId);
        Task<string> IsBlockedUserAsync(string userId);
    }
}
