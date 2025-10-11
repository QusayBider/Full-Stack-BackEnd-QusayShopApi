using Mapster;
using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.DTO.Requests;
using QusayShopApi.DAL.Models.Review;
using QusayShopApi.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.BLL.Services.Classes
{
    public class ReviewService : IReviewService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IOrderRepository orderRepository,IReviewRepository reviewRepository)
        {
            _orderRepository = orderRepository;
            _reviewRepository = reviewRepository;
        }
        public async Task<string> AddReviewAsync(ReviewDTORequest reviewDTORequest, string UserId)
        {
            var UserIsValidForReview = await _orderRepository.UserHasApprovedOrderForProductAsync(UserId,reviewDTORequest.ProductId);
            if (!UserIsValidForReview) { 
                return "You can't add review for a product you haven't purchased or your order is not approved yet.";
            }

            var UserIsReviewedBefore = await _reviewRepository.UserHasReviewedProductBeforeAsync(UserId, reviewDTORequest.ProductId);
            if (UserIsReviewedBefore) { 
                
                return "You have already reviewed this product.";
            }

            var ReviewRequest = reviewDTORequest.Adapt<Review>();
            
            
            return await _reviewRepository.AddReviewAsync(ReviewRequest ,UserId);  

        }
    }
}
