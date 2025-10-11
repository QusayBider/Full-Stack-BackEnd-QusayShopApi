using QusayShopApi.DAL.DTO.Requests;
using QusayShopApi.DAL.Models.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.Repositories.Interfaces
{
    public interface IReviewRepository
    {
       Task<bool> UserHasReviewedProductBeforeAsync(string userId, int productId);
       Task<String> AddReviewAsync(Review reviewRequest, string userId);
    }
}
