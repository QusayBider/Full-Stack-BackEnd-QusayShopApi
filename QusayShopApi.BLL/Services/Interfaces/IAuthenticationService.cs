using QusayShopApi.DAL.DTO.Requests;
using QusayShopApi.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.BLL.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<UserDTOResponse> LoginAsync(LoginDTORequest loginDTORequest);
        public Task<UserDTOResponse> RegisterAsync(RegisterDTORequest registerDTORequest);
        public  Task<string> ConfirmEmail(string token, string userId);
        public  Task<string> ForgetPassword(ForgetPasswordDTORequest request);
        public  Task<string> ResetPassword(ResetPasswordDTORequest request);

        }
}
