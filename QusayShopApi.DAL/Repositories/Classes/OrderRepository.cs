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
        public async Task<List<Order>> GetOrdersByStatus(OrderStatus Status) { 
             return await _context.Order.Where(o => o.Status == Status).OrderByDescending(O => O.OrderDate).ToListAsync();
        }
        public async Task <List<Order>> GetOrdersByUserAsync(string userId) { 
            return await _context.Order.Where(o => o.UserId == userId).OrderByDescending(o=>o.OrderDate).ToListAsync();
        }
        public async Task<string> ChangeStatusOfOrderAsync(int orderId, OrderStatus NewStatus) { 

            var order = await _context.Order.FindAsync(orderId);
            if (order == null) {
                return "Order Not Found";
            }
            order.Status = NewStatus;
            if (NewStatus == OrderStatus.Shipped) {
                order.ShippingDate = DateTime.UtcNow;
            }
            _context.Order.Update(order);
            var result =await _context.SaveChangesAsync();
            if (result <= 0) {
                return "Failed To Update Order Status";
            }
            return "Order Status Updated Successfully";
        }
        public async Task<bool> UserHasApprovedOrderForProductAsync(string userId,int productId) { 
            return await _context.Order.Include(o => o.OrderItems)
                .AnyAsync(o => o.UserId == userId && o.Status == OrderStatus.Approved && o.OrderItems.Any(oi => oi.ProductId == productId));
        }
        public Task<Order> GetOrderByIdAsync(int id)
        {
            return _context.Order.Include(u=>u.User).Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
        
    }
}
