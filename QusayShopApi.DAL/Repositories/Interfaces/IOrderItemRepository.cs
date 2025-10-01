using QusayShopApi.DAL.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        Task AddOrderItemsAsync(List<OrderItem> orderItems);
    }
}
