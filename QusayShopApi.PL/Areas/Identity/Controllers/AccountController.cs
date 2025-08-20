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
    }
}
