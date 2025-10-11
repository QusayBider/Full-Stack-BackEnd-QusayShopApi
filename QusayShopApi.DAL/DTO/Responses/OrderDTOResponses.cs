using QusayShopApi.DAL.Models;
using QusayShopApi.DAL.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.DTO.Responses
{
    public class OrderDTOResponses
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public DateTime? ShippingDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string? PaymentId { get; set; }
        public string? CarrierName { get; set; }
        public string? TrackingNumber { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; } = new();
    }
}
