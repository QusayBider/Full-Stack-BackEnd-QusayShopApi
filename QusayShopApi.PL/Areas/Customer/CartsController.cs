using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.DTO.Requests;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QusayShopApi.PL.Areas.Customer
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddToCart(CartDTORequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.addToCart(request, userId);
            if (!result)
            {
                return BadRequest("Could not add item to cart.");
            }
            return Ok("Item added to cart successfully.");
        }
        [HttpGet("")]
        public IActionResult GetCartSummary()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartSummary = _cartService.getCartSummary(userId);
            return Ok(cartSummary);
        }
    }
}
