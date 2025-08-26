using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.DTO.Requests;

namespace QusayShopApi.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles= "Admin,SuperAdmin")]
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
            var categories = _brandService.GetAll(true);
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
        [HttpPost("")]
        public IActionResult CreateBrand([FromBody] BrandDTORequest request)
        {
            var id = _brandService.Create(request);
            return CreatedAtAction(nameof(GetBrandById), new { id }, new { message = request });
        }
        [HttpPatch("{id}")]
        public IActionResult UpdateBrand([FromRoute] int id, [FromBody] BrandDTORequest request)
        {
            var result = _brandService.Update(id, request);
            if (result == 0) return NotFound();
            return Ok();
        }
        [HttpPatch("ToggleStatus/{id}")]
        public IActionResult ToggleStatus([FromRoute] int id)
        {
            var result = _brandService.ToggleStatus(id);
            if (!result) return NotFound();
            return Ok();
        }
        [HttpDelete("")]
        public IActionResult DeleteBrand([FromQuery] int id)
        {
            var result = _brandService.Delete(id);
            if (result == 0) return NotFound();
            return Ok();
        }
    }
}
