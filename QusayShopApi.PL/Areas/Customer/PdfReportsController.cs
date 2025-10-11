using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QusayShopApi.BLL.Services.Classes;
using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.Models;
using System.Security.Claims;

namespace QusayShopApi.PL.Areas.Customer.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class PdfReportsController : ControllerBase

    {
        private readonly PdfReportService _pdfReportService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderService _orderService;

        public PdfReportsController(PdfReportService pdfReportService, UserManager<ApplicationUser> userManager,IOrderService orderService)
        {
            _pdfReportService = pdfReportService;
            _userManager = userManager;
            _orderService = orderService;
        }

        [HttpGet("GeneratePdfReportForOrder")]
        public async Task<IResult> GeneratePdfReportForOrder([FromQuery] int OrderNumber)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Order = await _orderService.GetOrderByIdAsync(OrderNumber);
            if (Order == null) { 
             return Results.NotFound("Order Not Found");
            }
            var UserFind = await _userManager.FindByIdAsync(UserId);
            var document = _pdfReportService.CreateDocumentForOrder("System", OrderNumber);
            var pdf = document.GeneratePdf();
            return Results.File(pdf, "application/pdf", $"Order:{OrderNumber}-Report(Pdf).pdf");
        }
    }
}
