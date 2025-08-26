using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.DTO.Requests;
using QusayShopApi.DAL.DTO.Responses;
using QusayShopApi.DAL.Models;
using QusayShopApi.DAL.Utils;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.BLL.Services.Classes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public AuthenticationService(UserManager<ApplicationUser> userManager,IConfiguration configuration,IEmailSender emailSender ) {
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
        }

        public async Task<UserDTOResponse> LoginAsync(LoginDTORequest loginDTORequest)
        {
            var user = await _userManager.FindByEmailAsync(loginDTORequest.Email);
            if (user is  null)
            {
                throw new Exception("Invalid email or password");
            }
            var isValidPassword =await _userManager.CheckPasswordAsync(user,loginDTORequest.Password);

            if (!isValidPassword)
            {
                throw new Exception("Invalid email or password");
            }
            if (!user.EmailConfirmed) {
                throw new Exception("Please confirm your email");
            }
            return new UserDTOResponse
            {
                Token = await GeneretateJWT(user)
            };

        }

        private async Task<string> GeneretateJWT(ApplicationUser user) {

            var Claims = new List<Claim>() {
                new Claim("Name",user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };

            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles) {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConnection")["securityKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: Claims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

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
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var escapeToken = Uri.EscapeDataString(token);
                var emailUrl = $"https://localhost:7017/api/Identity/Account/confirmEmail?token={escapeToken}&userId={user.Id}";
                await _emailSender.SendEmailAsync(user.Email,"Welcome",$"<h1>Hello {user.UserName}</h1>"+$"<a href='{emailUrl}'>confirm</a>");
                return new UserDTOResponse()
                {
                    Token = registerDTORequest.Email
                };
            }
            else {
                throw new Exception($"{Result.Errors}");
            }
        }

        public async Task<string> ConfirmEmail(string token,string userId) { 
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new Exception("user not found"); 
            }
            var result = await _userManager.ConfirmEmailAsync(user,token);
            if (result.Succeeded) {
                return "Email confirmed succesfully";
            }
            return "Email confirmation failed";
        }

        public async Task<string> ForgetPassword(ForgetPasswordDTORequest request) {

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null) {
                throw new Exception("User not found");
            }

            var random=new Random();
            var code = random.Next(10000,99999).ToString();

            user.PasswordResetCode = code;
            user.PasswordResetCodeExpiredDate= DateTime.UtcNow.AddMinutes(10);

            await _userManager.UpdateAsync(user);
            await _emailSender.SendEmailAsync(request.Email,"Reset your password",$" <p style=\"margin:0 0 12px 0;font-size:14px;line-height:1.6;\"> Hi {user.UserName}, use the code below to reset your Account password:</p>" +$" <div class=\"code\" style=\"letter-spacing:4px;text-align:center;font-family:Consolas,Menlo,Monaco,monospace;font-weight:700;font-size:28px;padding:14px 20px;border:1px dashed #d1d5db;border-radius:10px;background:#f8fafc;color:#111827;margin:10px 0 14px 0;\">{code}</div>"+ $"<p class=\"muted\" style=\"margin:0 0 8px 0;font-size:12px;line-height:1.6;color:#6b7280;\">This code expires in <strong>10 minutes</strong> and can be used once. Do not share it with anyone.</p>" +$"<p class=\"muted\" style=\"margin:0 0 16px 0;font-size:12px;line-height:1.6;color:#6b7280;\">If you didn’t request a password reset, you can safely ignore this email.</p>");
            return ("Check your email — we’ve sent you a message with instructions to reset your password.");
        }
        public async Task<string> ResetPassword(ResetPasswordDTORequest request)
        {

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return("User not found");
            }
            if (user.PasswordResetCode != request.Code) {
                return("The code is not correct ");
            }
            if (user.PasswordResetCodeExpiredDate < DateTime.UtcNow) {
                return("The code is not correct ");
            }
            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (passwordValid)
            {
                return ("Your new password cannot be the same as your previous password. Please choose a different one.");
            }

            var token=await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user,token,request.Password);

            if (result.Succeeded)
            {
                await _emailSender.SendEmailAsync(user.Email, "Password Changed", $"<p>Hello {user.UserName},</p>" + $"<p>This is a confirmation that the password for your <b>QusayShop</b> account was successfully changed.</p>" + $"<p>If you made this change, no further action is required.<br>If you did not request this change, please reset your password immediately and contact our support team.</p>" + "<p style=\"color:#6b7280;font-size:12px;margin-top:20px;\">Thanks,<br>The {QusayShop} Team</p>");

                user.PasswordResetCode = "";
                await _userManager.UpdateAsync(user);
                return ("Password Changed Successfully");
            }
            else {
                return ("Password Changed Falid");
            }
            
        }

    }
}
