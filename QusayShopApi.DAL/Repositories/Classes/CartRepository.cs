using Microsoft.EntityFrameworkCore;
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
    }
}
