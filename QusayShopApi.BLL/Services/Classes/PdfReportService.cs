using Microsoft.AspNet.Identity;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.BLL.Services.Classes
{
    public class PdfReportService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderService _orderService;

        public PdfReportService(IProductRepository productRepository, IOrderService orderService)
        {
            _productRepository = productRepository;
            _orderService = orderService;
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public QuestPDF.Infrastructure.IDocument CreateDocumentForProduct(string UserName)
        {
            

            return Document.Create(container =>
            {
                container.Page(page =>
                {

                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11).FontFamily(Fonts.Arial));


                    page.Header()
                        .Text("📦 Qusay Shop — Products Report")
                        .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium)
                        .AlignCenter();


                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        col.Spacing(20);

                        foreach (var product in _productRepository.GetAllProductsWithImages())
                        {
                            col.Item().Border(1)
                                .BorderColor(Colors.Grey.Lighten2)
                                .Padding(10)
                                .Background(Colors.Grey.Lighten5)
                                .Column(item =>
                                {
                                    item.Spacing(6);

                                    
                                    item.Item().Text($"{product.Name}")
                                        .Bold().FontSize(16).FontColor(Colors.Blue.Darken2);

                                    item.Item().Text($"Description: {product.Description ?? "N/A"}");

                                    item.Item().Text(text =>
                                    {
                                        text.Span("Price: ").SemiBold();
                                        text.Span($"{product.Price:C}");
                                        if (product.Discount.HasValue)
                                        {
                                            text.Span($"   (Discount: {product.Discount.Value}%)").FontColor(Colors.Red.Medium);
                                        }
                                    });

                                    item.Item().Text($"Rating: ⭐ {product.Rating:F1}");
                                    item.Item().Text($"Quantity: {product.Quantity}");

                                    item.Item().Text($"Category: {product.Category?.Name ?? "N/A"}");
                                    item.Item().Text($"Brand: {product.Brand?.Name ?? "N/A"}");

                                    if (product.Reviews != null && product.Reviews.Any())
                                    {
                                        var avgRating = product.Reviews.Average(r => r.Rate);
                                        item.Item().Text($"🗨️ Reviews: {product.Reviews.Count} (Avg: {avgRating:F1}★)");
                                    }
                                    else
                                    {
                                        item.Item().Text("🗨️ No reviews yet").Italic().FontColor(Colors.Grey.Medium);
                                    }
                                });
                        }


                        col.Item().PaddingTop(40).Row(row =>
                        {
                            var stampPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "QusayBider_Stamp.png");

                            
                            row.RelativeItem().AlignLeft()
                                .Text($"Authorized by: {UserName}")
                                .Italic().FontSize(10);

                            if (File.Exists(stampPath))
                            {
                                row.ConstantItem(120) 
                                   .AlignRight()
                                   .Height(110)         
                                   .Image(stampPath)   
                                   .FitArea();         
                            }
                        });

                    });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                            x.Span(" of ");
                            x.TotalPages();
                        });
                });
            });
        }

        public QuestPDF.Infrastructure.IDocument CreateDocumentForOrder(string UserName, int orderNumber) {

            var order= _orderService.GetOrderByIdAsync(orderNumber).Result;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11).FontFamily(Fonts.Arial));

                    
                    page.Header()
                        .Text($"🧾 Qusay Shop — Order Details Report")
                        .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium)
                        .AlignCenter();

                    
                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        col.Spacing(15);

                        
                        col.Item().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingBottom(5)
                            .Text($"Order ID: #{order.Id}")
                            .Bold().FontSize(14).FontColor(Colors.Blue.Darken2);

                        col.Item().Text($"Date: {order.OrderDate:dd/MM/yyyy}");
                        col.Item().Text($"Status: {order.Status}");
                        col.Item().Text($"Total: {order.TotalAmount:C}");

                        
                        col.Item().PaddingTop(10).Column(cust =>
                        {
                            cust.Item().Text("👤 Customer Information").Bold().FontSize(13);
                            cust.Item().Text($"Name: {order.UserName ?? "N/A"}");
                            cust.Item().Text($"Email: {order.UserEmail ?? "N/A"}");
                        });

                        
                        if (order.OrderItems != null && order.OrderItems.Any())
                        {
                            col.Item().PaddingTop(15).Text("🛒 Products in this Order").Bold().FontSize(13);

                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(40);  
                                    columns.RelativeColumn(3);   
                                    columns.RelativeColumn(1);   
                                    columns.RelativeColumn(1);   
                                    columns.RelativeColumn(1);   
                                });

                                
                                table.Header(header =>
                                {
                                    header.Cell().Text("#").Bold();
                                    header.Cell().Text("Product").Bold();
                                    header.Cell().Text("Qty").Bold();
                                    header.Cell().Text("Price").Bold();
                                    header.Cell().Text("Subtotal").Bold();
                                });

                                
                                int index = 1;
                                foreach (var item in order.OrderItems)
                                {
                                    table.Cell().Text(index++);
                                    table.Cell().Text(item.ProductName?? "Unknown");
                                    table.Cell().Text(item.Quantity.ToString());
                                    table.Cell().Text($"{item.TotalPrice:C}");
                                    table.Cell().Text($"{(item.TotalPrice * item.Quantity):C}");
                                }
                            });
                        }
                        else
                        {
                            col.Item().PaddingTop(10)
                                .Text("No products in this order.")
                                .Italic().FontColor(Colors.Grey.Medium);
                        }

                        
                        col.Item().PaddingTop(40).Row(row =>
                        {
                            var stampPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "QusayBider_Stamp.png");

                            
                            row.RelativeItem().AlignLeft()
                                .Text($"Authorized by: {UserName}")
                                .Italic().FontSize(10);

                            
                            if (File.Exists(stampPath))
                            {
                                row.ConstantItem(120)
                                   .AlignRight()
                                   .Height(80)
                                   .Image(stampPath)
                                   .FitArea();
                            }
                        });
                    });

                    
                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                            x.Span(" of ");
                            x.TotalPages();
                        });
                });
            });
        }

    }
}

