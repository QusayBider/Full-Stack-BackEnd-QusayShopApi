using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QusayShopApi.DAL.Data;
using QusayShopApi.DAL.Models.Cart;
using QusayShopApi.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.Repositories.Classes
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context) {
            _context = context;
        }
        public async Task<int> addToCart(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            return await _context.SaveChangesAsync();

        }

        public async Task<List<Cart>> getCartItems(string userId)
        {
            return _context.Carts.Include(c=>c.Product).Where(c => c.UserId == userId).ToList();
        }
        public async Task<String> DeleteCart(string userId) { 
        
            var items = _context.Carts.Where(c => c.UserId == userId).ToList();

            if (items is null)
            {
                throw new Exception("No item in cart");
            }
            else {
                _context.Carts.RemoveRange(items);
                await _context.SaveChangesAsync();

                return ("The Cart clear successfully");

            }

        }

        public async Task<string> DeleteItemFromCart(string userId, int productId)
        {
            var item = _context.Carts.Where(c => c.UserId == userId && c.ProductId == productId);

            if (item is null) {
                throw new Exception("item not in cart");
            }
            else {
                _context.Carts.RemoveRange(item);
                await _context.SaveChangesAsync();
                 return ("item delete successfully");      
            }
        }
    }
}
