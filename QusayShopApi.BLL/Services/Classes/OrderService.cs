using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.DTO.Responses;
using QusayShopApi.DAL.Models.Order;
using QusayShopApi.DAL.Repositories.Classes;
using QusayShopApi.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.BLL.Services.Classes
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<string> ChangeStatusOfOrderAsync(int orderId, OrderStatus NewStatus)
        {
            return await _orderRepository.ChangeStatusOfOrderAsync(orderId, NewStatus);
        }
        public async Task<List<Order>> GetOrdersByUserAsync(string userId)
        {
            return await _orderRepository.GetOrdersByUserAsync(userId);
        }

        public async Task<List<Order>> GetOrdersByStatus(OrderStatus Status)
        {
            return await _orderRepository.GetOrdersByStatus(Status);
        }

        public async Task<OrderDTOResponses> GetOrderByIdAsync(int OrderId)
        {

            var order = await _orderRepository.GetOrderByIdAsync(OrderId);
            if (order == null) { 
                
                return null;
            }
            return new OrderDTOResponses
            {
                Id = order.Id,
                Status = order.Status,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                ShippingDate = order.ShippingDate,
                PaymentMethod = order.PaymentMethod,
                PaymentId = order.PaymentId,
                CarrierName = order.CarrierName,
                TrackingNumber = order.TrackingNumber,
                UserId = order.UserId,
                UserName = order.User?.FullName ?? "Unknown",
                UserEmail = order.User?.Email ?? "Unknown",
                OrderItems = order.OrderItems.Select(oi => new OrderItemDTO
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product?.Name ?? "Unknown",
                    ProductPrice = oi.Product?.Price ?? 0,
                    Quantity = oi.Quantity,
                    TotalPrice = oi.TotalPrice
                }).ToList()
            };


        }

        public async Task<OrderDTOResponses> GetOrderUserByIdAsync(string userId, int OrderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(OrderId);
            if(order == null || order.UserId != userId)
            {
                return null;
            }
            return new OrderDTOResponses
            {
                Id = order.Id,
                Status = order.Status,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                ShippingDate = order.ShippingDate,
                PaymentMethod = order.PaymentMethod,
                PaymentId = order.PaymentId,
                CarrierName = order.CarrierName,
                TrackingNumber = order.TrackingNumber,
                UserId = order.UserId,
                UserName = order.User?.FullName ?? "Unknown",
                UserEmail = order.User?.Email ?? "Unknown",
                OrderItems = order.OrderItems.Select(oi => new OrderItemDTO
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product?.Name ?? "Unknown",
                    ProductPrice = oi.Product?.Price ?? 0,
                    Quantity = oi.Quantity,
                    TotalPrice = oi.TotalPrice
                }).ToList()
            };
        }
    }
}
