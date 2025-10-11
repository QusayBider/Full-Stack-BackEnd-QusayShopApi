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

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<string> addToCart(CartDTORequest request, string UserId)
        {
            var CartItems = await _cartRepository.getCartItems(UserId);
            foreach (var item in CartItems)
            {
                if (item.ProductId == request.ProductId)
                {
                    return "Item already in cart";
                }
            }
            var newItem = new Cart
            {
                ProductId = request.ProductId,
                Quantity = 1,
                UserId = UserId
            };


            var returnValue = await _cartRepository.addToCart(newItem);
            if (returnValue > 0)
            {
                return "Item added to cart successfully.";
            }
            else
            {
                return "Could not add item to cart.";
            }
        }

        public async Task<CartSummaryDTOResponse> getCartSummary(string userId)
        {
            var cartItems = await _cartRepository.getCartItems(userId);
            var response = new CartSummaryDTOResponse()
            {
                Items = cartItems.Select(ci => new CartDTOResponse
                {
                    ProductId = ci.ProductId,
                    ProductName = ci.Product.Name,
                    Quantity = ci.Quantity,
                    Price = ci.Product.Price
                }).ToList()
            };
            return response;
        }

        public async Task<string> RemoveProductFromCart(int ProductId, string UserId)
        {
            return await _cartRepository.DeleteItemFromCart(UserId, ProductId);
        }
        public async Task<string> IncreaseCartItemQuantity(string userId, int productId, int quantity)
        {
            return await _cartRepository.IncreaseCartItemQuantity(userId, productId, quantity);
        }
        public async Task<string> DecreaseCartItemQuantity(string userId, int productId, int quantity)
        {
            return await _cartRepository.DecreaseCartItemQuantity(userId, productId, quantity);
        }
    }
}
