using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QusayShopApi.BLL.Services.Classes;
using QusayShopApi.DAL.Models;
using System.Security.Claims;

namespace QusayShopApi.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class PdfReportsController : ControllerBase

    {
        private readonly PdfReportService _pdfReportService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PdfReportsController(PdfReportService pdfReportService, UserManager<ApplicationUser> userManager)
        {
            _pdfReportService = pdfReportService;
            _userManager = userManager;
        }
        [HttpGet("GeneratePdfReportForProducts")]
        public async Task<IResult> GeneratePdfReportForProducts()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var UserFind = await _userManager.FindByIdAsync(UserId);
            var document = _pdfReportService.CreateDocumentForProduct(UserFind.FullName);


            var pdf = document.GeneratePdf();
            return Results.File(pdf, "application/pdf", "Products Report(Pdf).pdf");
        }

        [HttpGet("GeneratePdfReportForOrder")]
        public async Task<IResult> GeneratePdfReportForOrder([FromQuery] int OrderNumber)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var UserFind = await _userManager.FindByIdAsync(UserId);
            var document = _pdfReportService.CreateDocumentForOrder(UserFind.FullName, OrderNumber);
            var pdf = document.GeneratePdf();
            return Results.File(pdf, "application/pdf", $"Order:{OrderNumber}-Report(Pdf).pdf");
        }
    }
}
