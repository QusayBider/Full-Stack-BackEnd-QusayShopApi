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
            var categories = _brandService.GetAll(false);
            return Ok(categories);
        }
        [HttpGet("{id}")]

        //[Authorize] its use to be use this action only of the user login else well be return error
        public IActionResult GetBrandById([FromRoute] int id)
        {
            var _brand = _brandService.GetById(id);
            if (_brand is null) return NotFound();
            return Ok(_brand);
        }
    }
}
