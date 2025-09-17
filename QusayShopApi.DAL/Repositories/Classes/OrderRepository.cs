using Microsoft.EntityFrameworkCore;
using QusayShopApi.DAL.Data;
using QusayShopApi.DAL.Models.Order;
using QusayShopApi.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.Repositories.Classes
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<Order?> AddOrderAsync(Order order)
        {
             await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public Task<Order?> GetUserByOrderById(int id)
        {
            return _context.Order.Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
