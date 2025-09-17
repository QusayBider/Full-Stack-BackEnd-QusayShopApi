using QusayShopApi.DAL.DTO.Requests;
using QusayShopApi.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.BLL.Services.Interfaces
{
    public interface ICartService
    {
         Task<bool> addToCart(CartDTORequest request, string userId);
         Task<CartSummaryDTOResponse> getCartSummary(string userId);
    }
}
