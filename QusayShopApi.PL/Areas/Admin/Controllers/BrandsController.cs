using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QusayShopApi.BLL.Services.Classes;
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
        [HttpGet("GetAllBrands")]
        public async Task<IActionResult> GetAllBrands()
        {
            
            return Ok(await _brandService.GetAllProduct(Request,true));
        }
        [HttpGet("GetBrandById/{id}")]
        public async Task<IActionResult> GetBrandById([FromRoute] int id)
        {
            var _brand = await _brandService.GetBrandById(Request, id, true);
            if (_brand is null) return NotFound();
            return Ok(_brand);
        }
        [HttpPost("AddBrand")]
        public async Task<IActionResult> Create([FromForm] BrandDTORequest request)
        {
            var isExist = _brandService.checkedIfBrandHasExist(request.Name);
            if (isExist) return Conflict("Brand Is Already Exist");
            else
            {
                var result = await _brandService.CreateFile(request);
                return Ok(result);
            }
        }
        [HttpPatch("UpdateBrand/{id}")]
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
            return Ok("Operation completed successfully.");
        }
    }
}
