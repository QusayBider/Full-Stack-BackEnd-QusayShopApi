using QusayShopApi.DAL.DTO.Responses;
using QusayShopApi.DAL.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersByStatus(OrderStatus Status);
        Task<List<Order>> GetOrdersByUserAsync(string userId);
        Task<string> ChangeStatusOfOrderAsync(int orderId, OrderStatus NewStatus);
        Task<OrderDTOResponses> GetOrderByIdAsync(int orderId);
    }
}
