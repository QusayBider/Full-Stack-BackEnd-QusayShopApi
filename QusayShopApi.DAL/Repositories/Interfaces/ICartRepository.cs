using QusayShopApi.DAL.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.Repositories.Interfaces
{
    public interface ICartRepository
    {
         Task<int> addToCart(Cart cart);
         Task<List<Cart>> getCartItems(string userId);
         Task<String> DeleteCart(string userId);
        Task<String> DeleteItemFromCart(string userId,int productId);

    }
}
