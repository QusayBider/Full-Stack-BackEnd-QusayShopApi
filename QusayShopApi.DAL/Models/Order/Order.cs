using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.Models.Order
{
    public enum OrderStatus
    {
        Pending=1,
        Approved=2,
        Shipped=3,
        Delivered=4,
        Cancelled=5
    }
    public enum PaymentMethod
    {
        Cash=1,
        Visa=2
    
    }
    public class Order
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public decimal TotalAmount { get; set; }
        public DateTime? ShippingDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; } 
        public string? PaymentId { get; set; }
        public string ? CarrierName { get; set; }
        public string ? TrackingNumber { get; set; }

        public string UserId { get; set; } 
        public ApplicationUser User { get; set; }
    }
}
