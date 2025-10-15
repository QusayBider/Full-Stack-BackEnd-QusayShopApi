using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QusayShopApi.BLL.Services.Interfaces;
using System.Security.Claims;

namespace QusayShopApi.PL.Areas.Customer
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet("GetMyOrders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
            var result = await _orderService.GetOrdersByUserAsync(userId);
            return Ok(result);
        }
        [HttpPost("GetMyOrderById{OrderId}")]
        public async Task<IActionResult> GetMyOrderById([FromRoute] int OrderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
            var result = await _orderService.GetOrderUserByIdAsync(userId, OrderId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);

        }
    }
}
