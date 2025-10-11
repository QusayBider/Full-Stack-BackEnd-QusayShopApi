using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.Models.Order
{
    [PrimaryKey(nameof(OrderId),nameof(ProductId))]
    public class OrderItem
    {
        public int OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product.Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
