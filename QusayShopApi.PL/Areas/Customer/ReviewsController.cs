using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.DTO.Requests;
using QusayShopApi.DAL.Repositories.Interfaces;
using System.Security.Claims;

namespace QusayShopApi.PL.Areas.Customer
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService ) {
            _reviewService = reviewService;
        }
        [HttpPost("")]
        public async Task<IActionResult> AddReview([FromBody] ReviewDTORequest reviewDTORequest) {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _reviewService.AddReviewAsync(reviewDTORequest, UserId);
            return Ok(result);
        
        }
    }
}
