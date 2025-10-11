using Microsoft.AspNetCore.Mvc;
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
         Task<string> addToCart(CartDTORequest request, string userId);
         Task<CartSummaryDTOResponse> getCartSummary(string userId);
         Task<string> RemoveProductFromCart(int ProductId, string UserId);
         Task<string> IncreaseCartItemQuantity(string userId, int productId, int quantity);
         Task<string> DecreaseCartItemQuantity(string userId, int productId, int quantity);
    }
}
