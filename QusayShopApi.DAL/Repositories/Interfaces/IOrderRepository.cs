using QusayShopApi.DAL.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetUserByOrderById(int id);
        Task<Order?> AddOrderAsync(Order order);
        Task<List<Order>> GetOrdersByStatus(OrderStatus Status);
        Task<List<Order>> GetOrdersByUserAsync(string userId);
        Task<string> ChangeStatusOfOrderAsync(int orderId, OrderStatus NewStatus);
        Task<bool> UserHasApprovedOrderForProductAsync(string userId, int productId);
        Task<Order> GetOrderByIdAsync(int id);
    }
}
