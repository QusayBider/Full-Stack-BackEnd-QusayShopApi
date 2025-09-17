using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.DTO.Requests;
using QusayShopApi.DAL.DTO.Responses;
using QusayShopApi.DAL.Models.Cart;
using QusayShopApi.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.BLL.Services.Classes
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public  CartService(ICartRepository cartRepository) {
            _cartRepository = cartRepository;
        }
        public async Task<bool> addToCart(CartDTORequest request, string UserId)
        {

            var newItem = new Cart {
                ProductId = request.ProductId,
                Quantity = 1,
                UserId = UserId
            };

             
            return await _cartRepository.addToCart(newItem) >0;
        }

        public async Task<CartSummaryDTOResponse> getCartSummary(string userId)
        {
            var cartItems = await _cartRepository.getCartItems(userId);
            var response = new CartSummaryDTOResponse() { 
            Items =  cartItems.Select(ci => new CartDTOResponse {
                ProductId = ci.ProductId,
                ProductName = ci.Product.Name,
                Quantity = ci.Quantity,
                Price = ci.Product.Price
            }).ToList()
            };
            return  response;
        }
    }
}
