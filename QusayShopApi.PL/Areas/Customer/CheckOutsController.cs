using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.DTO.Requests;
using Stripe;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QusayShopApi.PL.Areas.Customer
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class CheckOutsController : ControllerBase
    {
        private readonly ICheckOutService _checkOutService;

        public CheckOutsController(ICheckOutService checkOutService)
        {
            _checkOutService = checkOutService;
        }
        [HttpPost("Payment")]
        public async Task<IActionResult> Payment(CheckOutDTORequest checkOutDTORequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _checkOutService.ProcessPaymentAsync(checkOutDTORequest,userId,Request);
            return Ok(result);
        }
        [HttpGet("Success/{orderId}")]
        [AllowAnonymous]
        public async Task<IActionResult> Success([FromRoute]int orderId)
        {
            var result = await _checkOutService.HandelPaymentSuccessAsync(orderId);
            return Ok(result);
        }
    }
}
