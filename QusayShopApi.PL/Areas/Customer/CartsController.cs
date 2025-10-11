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

        [HttpPost("AddProductsToCart")]
        public async Task<IActionResult> AddToCart(CartDTORequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.addToCart(request, userId);
            return Ok(result);
        }
        [HttpGet("GetCart")]
        public IActionResult GetCartSummary()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartSummary = _cartService.getCartSummary(userId);
            return Ok(cartSummary);
        }
        [HttpDelete("DeleteProductFromCart/{ProductId}")]
        public async Task<IActionResult> RemoveProductFromCart([FromRoute]int ProductId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.RemoveProductFromCart(ProductId, userId);
            return Ok(result);
        }
        [HttpPost("IncreaseCartItemQuantity/{ProductId}")]
        public async Task<IActionResult> IncreaseCartItemQuantity([FromRoute]int ProductId,[FromQuery] int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.IncreaseCartItemQuantity(userId, ProductId, quantity);
            return Ok(result);
        }
        [HttpPost("DecreaseCartItemQuantity/{ProductId}")]
        public async Task<IActionResult> DecreaseCartItemQuantity([FromRoute] int ProductId, [FromQuery] int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.DecreaseCartItemQuantity(userId, ProductId, quantity);
            return Ok(result);
        }
    }
}
