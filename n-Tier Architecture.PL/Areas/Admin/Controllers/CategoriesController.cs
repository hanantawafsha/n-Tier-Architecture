using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTierArchitecture.BLL.Services.Interfaces;
using NTierArchitecture.DAL.DTO.Requests;

namespace NTierArchitecture.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("")]
        public IActionResult GetAll() => Ok(_categoryService.GetAll());

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var category = _categoryService.GetById(id);
            if (category is null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public IActionResult Create([FromBody] CategoryRequest request)
        {
            var id = _categoryService.Create(request);
            return CreatedAtAction(nameof(Get), new { id });
        }
        [HttpPatch("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] CategoryRequest request)
        {
            var updated = _categoryService.Update(id, request);
            return updated > 0 ? Ok() : NotFound();
        }
        [HttpPatch("ToggleStatus/{id}")]
        public IActionResult ToggleStatus([FromRoute] int id)
        {
            var updated = _categoryService.ToggleStatus(id);
            return updated ? Ok(new { message = "Status togglled" }) : NotFound(new { message = "Categroy not found" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var deleted = _categoryService.Delete(id);
            return deleted > 0 ? Ok() : NotFound();
        }
    }
}
