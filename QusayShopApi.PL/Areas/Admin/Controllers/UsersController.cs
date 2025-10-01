using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QusayShopApi.BLL.Services.Interfaces;

namespace QusayShopApi.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GettAllUserAsync();
            return Ok(users);
        }
        [HttpPatch("Block/{userId}")]

        public async Task<IActionResult> BlockUser([FromRoute] string userId, [FromQuery] int days)
        {
            var result = await _userService.BlockUserAsync(userId, days);
            return Ok(result);
        }

        [HttpPatch("UnBlock/{userId}")]
        public async Task<IActionResult> UnBlockUser([FromRoute] string userId)
        {
            var result = await _userService.UnBlockUserAsymc(userId);
            return Ok(result);
        }
        [HttpGet("IsBlocked/{userId}")]
        public async Task<IActionResult> IsBlockedUser([FromRoute] string userId)
        {
            var result = await _userService.IsBlockedUserAsync(userId);
            return Ok(result);
        }
    }
}
