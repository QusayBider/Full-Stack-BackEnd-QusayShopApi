using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.DTO.Requests;
using QusayShopApi.DAL.Models.Order;

namespace QusayShopApi.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet("Get-orders-by-status/{status}")]
        public async Task<IActionResult> GetOrdersByStatus([FromRoute] OrderStatus status)
        {
            var orders = await _orderService.GetOrdersByStatus(status);
            return Ok(orders);
        }
        [HttpPost("Change-order-status/{orderId}")]
        public async Task<IActionResult> ChangeOrderStatus([FromRoute] int orderId, [FromBody]OrderStatusDTORequest request)
        {
            var result = await _orderService.ChangeStatusOfOrderAsync(orderId, request.NewStatus);
            return Ok(result);
        }
        [HttpGet("Get-all-orders-by-user/{UserId}")]
        public async Task<IActionResult> GetAllOrdersByUser([FromRoute]string UserId)
        {
            var orders = await _orderService.GetOrdersByUserAsync(UserId);
            return Ok(orders);
        }

        [HttpGet("GetOrderById")]
        public async Task<IActionResult> GetOrderById([FromQuery] int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound("Order Not Found");
            }
            return Ok(order);
        }
    }
}
