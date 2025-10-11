using Microsoft.EntityFrameworkCore;
using QusayShopApi.DAL.Data;
using QusayShopApi.DAL.Models.Review;
using QusayShopApi.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.Repositories.Classes
{
    public class ReviewRepository :IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserHasReviewedProductBeforeAsync(string userId, int productId)
        {
            return await _context.Reviews.AnyAsync(r => r.UserId == userId && r.ProductId == productId);
        }
        public async Task<String> AddReviewAsync(Review reviewRequest,string userId) {

            reviewRequest.UserId = userId;
            reviewRequest.ReviewDate = DateTime.Now;

            await  _context.Reviews.AddAsync(reviewRequest);
            await _context.SaveChangesAsync();
            return "Review Added Successfully";
        }
    }
}
