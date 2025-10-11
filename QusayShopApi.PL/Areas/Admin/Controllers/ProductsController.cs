using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QusayShopApi.BLL.Services.Classes;
using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.DTO.Requests;
using System.Threading.Tasks;

namespace QusayShopApi.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost("AddProduct")]
        public async Task<IActionResult> Create([FromForm] ProductDTORequest request)
        {
            var result= await _productService.AddProduct(request);
            return Ok(result);
        }
        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts([FromQuery] int PageSize=0, [FromQuery] int NumberOfPage=0)
        {
            var products = _productService.GetAllProductsWithImages(Request, NumberOfPage, PageSize, false);
            return Ok(products);
        }
        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            return Ok(product);
        }
        [HttpPatch("ToggleStatus/{id}")]
        public IActionResult ToggleStatus([FromRoute] int id)
        {
            var result = _productService.ToggleStatus(id);
            if (!result) return NotFound();
            return Ok("Statu Changed");
        }
        [HttpDelete("{ProductId}")]
        public Task<string> DeleteProduct([FromRoute] int ProductId)
        {
            return _productService.DeleteProduct(ProductId);
        }


    }
}
