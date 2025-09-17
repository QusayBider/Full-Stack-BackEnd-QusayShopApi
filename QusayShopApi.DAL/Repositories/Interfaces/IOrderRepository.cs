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
    }
}
