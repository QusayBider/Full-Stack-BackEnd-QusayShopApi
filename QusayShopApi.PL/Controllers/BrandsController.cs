using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QusayShopApi.BLL.Services;
using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.DTO.Requests;

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
        public IActionResult GetAllBrands()
        {
            var Brands = _brandService.GetAll(false);
            if (!Brands.Any())
            {
                return BadRequest("There are no activated brands.");
            }
            return Ok(Brands);
        }
        [HttpGet("{id}")]

        public IActionResult GetBrandById([FromRoute] int id)
        {
            var _brand = _brandService.GetById(id);
            if (_brand is null) return NotFound();
            return Ok(_brand);
        }
    }
}
