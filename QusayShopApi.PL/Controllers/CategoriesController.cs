using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.DTO.Requests;

namespace QusayShopApi.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Customer")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices _categoryService;

        public CategoriesController(ICategoryServices categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("")]
        public IActionResult GetAllCategories()
        {
            var categories = _categoryService.GetAll();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public IActionResult GetCategoryById([FromRoute] int id)
        {
            var category = _categoryService.GetById(id);
            if (category is null) return NotFound();
            return Ok(category);
        }
        [HttpPost("")]
        public IActionResult CreateCategory([FromBody] CategoryDTORequest request)
        {
            var id = _categoryService.Create(request);
            return CreatedAtAction(nameof(GetCategoryById), new { id }, new {message= request });
        }
        [HttpPatch("{id}")]
        public IActionResult UpdateCategory([FromRoute] int id, [FromBody] CategoryDTORequest request)
        {
            var result = _categoryService.Update(id, request);
            if (result == 0) return NotFound();
            return Ok();
        }
        [HttpPatch("ToggleStatus/{id}")]
        public IActionResult ToggleStatus([FromRoute] int id)
        {
            var result = _categoryService.ToggleStatus(id);
            if (!result) return NotFound();
            return Ok();
        }
        [HttpDelete("")]
        public IActionResult DeleteCategory([FromQuery] int id)
        {
            var result = _categoryService.Delete(id);
            if (result == 0) return NotFound();
            return Ok();
        }
    }
}
