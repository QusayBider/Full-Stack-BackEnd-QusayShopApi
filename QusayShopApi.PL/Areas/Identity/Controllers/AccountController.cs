using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.DTO.Requests;
using QusayShopApi.DAL.DTO.Responses;

namespace QusayShopApi.PL.Areas.Identity.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Identity")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        } 
        [HttpPost("Register")]
        public async Task<ActionResult< UserDTOResponse>> Register([FromBody] RegisterDTORequest registerDTORequest)
        {
            //ActionResult< UserDTOResponse> it use to force to return a spicfic data in the body of the request
            var result = await _authenticationService.RegisterAsync(registerDTORequest);
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTOResponse>> Login([FromBody] LoginDTORequest loginDTORequest)
        {
            var result = await _authenticationService.LoginAsync(loginDTORequest);
            return Ok(result);
        }
        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult<string>> ConfirmEmail([FromQuery] string token, [FromQuery] string userId)
        {
            var result = await _authenticationService.ConfirmEmail(token, userId);
            return Ok(result);
        }
        [HttpPost("ForgetPassword")]
        public async Task<ActionResult<string>> ForgetPassword([FromBody] ForgetPasswordDTORequest request)
        {
            var result = await _authenticationService.ForgetPassword(request);
            return Ok(result);
        }

        [HttpPatch("ResetPassword")]
        public async Task<ActionResult<string>> ResetPassword([FromBody] ResetPasswordDTORequest request)
        {
            var result = await _authenticationService.ResetPassword(request);
            return Ok(result);
        }
    }
}
