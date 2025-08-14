using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QusayShopApi.BLL.Services;
using QusayShopApi.DAL.DTO.Requests;

namespace QusayShopApi.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices categoryService;

        public CategoriesController(ICategoryServices categoryService)
        {
            this.categoryService = categoryService;
        }
        [HttpGet("")]
        public IActionResult GetAllCategories()
        {
            var categories = categoryService.GetAllCategories();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public IActionResult GetCategoryById([FromRoute] int id)
        {
            var category = categoryService.GetCategoryById(id);
            if (category is null) return NotFound();
            return Ok(category);
        }
        [HttpPost("")]
        public IActionResult CreateCategory([FromBody] CategoryDTORequest request)
        {
            var id = categoryService.CreateCategory(request);
            return CreatedAtAction(nameof(GetCategoryById), new { id });
        }
        [HttpPatch("{id}")]
        public IActionResult UpdateCategory([FromRoute] int id, [FromBody] CategoryDTORequest request)
        {
            var result = categoryService.UpdateCategory(id, request);
            if (result == 0) return NotFound();
            return Ok();
        }
        [HttpPatch("ToggleStatus/{id}")]
        public IActionResult ToggleStatus([FromRoute] int id)
        {
            var result = categoryService.ToggleStatus(id);
            if (!result) return NotFound();
            return Ok();
        }
        [HttpDelete("")]
        public IActionResult DeleteCategory([FromQuery] int id)
        {
            var result = categoryService.DeleteCategory(id);
            if (result == 0) return NotFound();
            return Ok();
        }
    }
}
