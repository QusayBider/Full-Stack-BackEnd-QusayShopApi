using Microsoft.AspNetCore.Identity;
using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.DTO.Requests;
using QusayShopApi.DAL.DTO.Responses;
using QusayShopApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.BLL.Services.Classes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
        }

        public async Task<UserDTOResponse> LoginAsync(LoginDTORequest loginDTORequest)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTOResponse> RegisterAsync(RegisterDTORequest registerDTORequest)
        {
            var user = new ApplicationUser { 
                
                UserName = registerDTORequest.UserName,
                FullName = registerDTORequest.FullName,
                Email = registerDTORequest.Email,
                PhoneNumber = registerDTORequest.PhoneNumber

            };
           var Result= await _userManager.CreateAsync(user, registerDTORequest.Password);
            if (Result.Succeeded)
            {
                return new UserDTOResponse()
                {
                    Email = registerDTORequest.Email
                };
            }
            else {
                throw new Exception($"{Result.Errors}");
            }
        }
    }
}
