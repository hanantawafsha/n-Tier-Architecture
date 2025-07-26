using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using n_Tier_Architecture.BLL.Services;
using n_Tier_Architecture.DAL.DTO.Requests;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace n_Tier_Architecture.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService) {
            this.categoryService = categoryService;
        }
        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(categoryService.GetAllCategories());
        }
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var category = categoryService.GetCategoryById(id);
            if (category is null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public IActionResult Create([FromBody] CategoryRequest request)
        {
           var id = categoryService.CreateCategory(request);
            return CreatedAtAction(nameof(Get), new {id});
        }
        [HttpPatch("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] CategoryRequest request)
        {
            var updated = categoryService.UpdateCategory(id, request);
            return updated >0 ? Ok() : NotFound();
        }
        [HttpPatch("ToggleStatus/{id}")]
        public IActionResult ToggleStatus([FromRoute] int id)
        {
            var updated = categoryService.ToggleStatus(id);
            return updated ? Ok(new {message ="Status togglled"}) : NotFound(new {message="Categroy not found"});
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var deleted = categoryService.DeleteCategory(id);
            return deleted > 0 ? Ok() : NotFound();
        }
    }
}
