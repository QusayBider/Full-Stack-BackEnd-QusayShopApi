using QusayShopApi.DAL.DTO.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.BLL.Services.Interfaces
{
    public interface IReviewService
    {
        Task<string> AddReviewAsync(ReviewDTORequest reviewDTORequest,String UserId);
    }
}
