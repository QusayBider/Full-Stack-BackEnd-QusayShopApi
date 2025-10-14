using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QusayShopApi.BLL.Services;
using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.DTO.Requests;
using System.Threading.Tasks;

namespace QusayShopApi.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandServices _brandService;

        public BrandsController(IBrandServices _brandService)
        {
            this._brandService = _brandService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllBrands()
        {
            var Brands = await _brandService.GetAllProduct(Request,false);
            if (!Brands.Any())
            {
                return BadRequest("There are no activated brands.");
            }
            return Ok(Brands);
        }
        [HttpGet("GetBrandById/{id}")]

        public async Task<IActionResult> GetBrandById([FromRoute] int id)
        {
            var _brand =await  _brandService.GetBrandById(Request, id, false);
            if (_brand is null) return NotFound();
            return Ok(_brand);
        }
    }
}
