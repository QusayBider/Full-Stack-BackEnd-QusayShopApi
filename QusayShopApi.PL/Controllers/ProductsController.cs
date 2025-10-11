using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QusayShopApi.BLL.Services.Interfaces;

namespace QusayShopApi.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts([FromQuery]int NumberOfPage = 0, [FromQuery] int PageSize = 0)
        {
            var products = await _productService.GetAllProductsWithImages(Request, NumberOfPage, PageSize, true);
            return Ok(products);
        }
    }
}
