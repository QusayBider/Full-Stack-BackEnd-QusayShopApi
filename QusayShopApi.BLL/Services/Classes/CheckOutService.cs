using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.DTO.Requests;
using QusayShopApi.DAL.DTO.Responses;
using QusayShopApi.DAL.Models.Order;
using QusayShopApi.DAL.Repositories.Interfaces;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.BLL.Services.Classes
{
    public class CheckOutService : ICheckOutService
    {
        private readonly ICheckOutRepository _checkOutRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailSender _emailSender;
        private readonly IOrderItemRepository _orderItemRepository;

        public CheckOutService(ICheckOutRepository checkOutRepository,ICartRepository cartRepository,IOrderRepository orderRepository,IEmailSender emailSender,IOrderItemRepository orderItemRepository)
        {
            _checkOutRepository = checkOutRepository;
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _emailSender = emailSender;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<bool> HandelPaymentSuccessAsync(int OrderId)
        {
            var order = await _orderRepository.GetUserByOrderById(OrderId);
            var subject = "";
            var body = @"
                    <!DOCTYPE html>
                    <html lang='en'>
                    <head>
                      <meta charset='UTF-8'>
                      <title>Payment Successful</title>
                    </head>
                    <body style='font-family: Arial, sans-serif; background-color:#f8f9fa; margin:0; padding:0;'>

                      <div style='max-width:600px; margin:20px auto; background:#ffffff; border-radius:8px; 
                                  box-shadow:0 4px 8px rgba(0,0,0,0.05); overflow:hidden;'>

                        <!-- Header -->
                        <div style='background:#0d6efd; color:#fff; padding:20px; text-align:center;'>
                          <h2 style='margin:0;'>✅ Payment Successful</h2>
                        </div>

                        <!-- Body -->
                        <div style='padding:20px;'>
                          <h4 style='color:#0d6efd; margin-top:0;'>Hello {customer_name},</h4>
                          <p>Your Visa payment has been processed successfully. Below are the details:</p>

                          <!-- Bootstrap-like card -->
                          <div style='border:1px solid #dee2e6; border-radius:6px; padding:15px; background:#f8f9fa; margin:15px 0;'>
                            <p style='margin:5px 0;'><strong>Transaction ID:</strong> {transaction_id}</p>
                            <p style='margin:5px 0;'><strong>Amount Paid:</strong> {amount} {currency}</p>
                            <p style='margin:5px 0;'><strong>Date:</strong> {payment_date}</p>
                          </div>

                          <p>Thank you for your purchase! If you have any questions, just reply to this email.</p>
                          <p style='margin-bottom:0;'>— The {company_name} Team</p>
                        </div>

                        <!-- Footer -->
                        <div style='background:#f1f1f1; color:#6c757d; text-align:center; padding:10px; font-size:12px;'>
                          &copy; {year} {company_name}. All rights reserved.
                        </div>

                      </div>
                    </body>
                    </html>";

            if (order.PaymentMethod == DAL.Models.Order.PaymentMethod.Visa) {
                order.Status = OrderStatus.Approved;
                var carts = await _cartRepository.getCartItems(order.UserId);
                var OrderItems = new List<OrderItem>();
                foreach (var item in carts)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        TotalPrice = item.Product.Price* item.Product.Quantity
                    };

                    OrderItems.Add(orderItem);
                }
                await _orderItemRepository.AddOrderItemsAsync(OrderItems);
                await _cartRepository.DeleteCart(order.UserId);

                subject = "Visa Payment Successful";
                body = body.Replace("{customer_name}", order.User.FullName)
                           .Replace("{transaction_id}", order.PaymentId ?? "N/A")
                           .Replace("{amount}", order.TotalAmount.ToString("F2"))
                           .Replace("{currency}", "USD")
                           .Replace("{payment_date}", order.OrderDate.ToString("MMMM dd, yyyy"))
                           .Replace("{company_name}", "QusayShop")
                           .Replace("{year}", DateTime.UtcNow.Year.ToString());
            }
            else if (order.PaymentMethod == DAL.Models.Order.PaymentMethod.Cash)
            {
                order.Status = OrderStatus.Pending;
                subject = "Order Placed Successfully";
                body = body.Replace("{customer_name}", order.User.FullName)
                          .Replace("{transaction_id}", $"{order.Id}")
                          .Replace("{amount}", order.TotalAmount.ToString("F2"))
                          .Replace("{currency}", "USD")
                          .Replace("{payment_date}", null)
                          .Replace("{company_name}", "QusayShop")
                          .Replace("{year}", DateTime.UtcNow.Year.ToString());
                return true;
            }

            await _emailSender.SendEmailAsync(order.User.Email, subject, body);
            return true;
        }

        public async Task<CheckOutDTOResponse> ProcessPaymentAsync(CheckOutDTORequest request, string UserId, HttpRequest Request)
        {
            var cartItems = await _cartRepository.getCartItems(UserId);
            if (!cartItems.Any())
            {
                return new CheckOutDTOResponse
                {
                    Success = false,
                    Message = "Cart is empty"

                };
            }
            Order Order = new Order
            {
                UserId = UserId,
                TotalAmount = cartItems.Sum(item => item.Product.Price * item.Quantity),
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                PaymentMethod = request.PaymentMethod,
                
            };
            await _orderRepository.AddOrderAsync(Order);
            if (request.PaymentMethod == DAL.Models.Order.PaymentMethod.Cash) {
                return new CheckOutDTOResponse
                {
                    Success = true,
                    Message = "Cash payment selected"
                };
            }
            if (request.PaymentMethod == DAL.Models.Order.PaymentMethod.Visa)
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>

                    {

                    },
                    Mode = "payment",
                    SuccessUrl = $"{Request.Scheme}://{Request.Host}/api/Customer/CheckOuts/Success/{Order.Id}",
                    CancelUrl = $"{Request.Scheme}://{Request.Host}/checkout/cancel",
                };
                foreach (var item in cartItems)
                {
                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name,
                                Description = item.Product.Description,
                            },
                            UnitAmount = (long)(item.Product.Price * 100),
                        },
                        Quantity = item.Quantity,
                    });
            }
            var service = new SessionService();
            var session = service.Create(options);

               Order.PaymentId = session.Id;

                return  new CheckOutDTOResponse 
            {

                Success = true,
                Message = "Payment processed successfully",
                PaymentId = session.Id,
                URL = session.Url
            };

        
            }
            return new CheckOutDTOResponse
            {
                Success = false,
                Message = "Invalid or unsupported payment method"
            }; 
 
        }
    }
}
