using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using n_Tier_Architecture.BLL.Services.Classes;
using n_Tier_Architecture.BLL.Services.Interfaces;
using n_Tier_Architecture.DAL.DTO.Requests;
using n_Tier_Architecture.DAL.DTO.Responses;
using System.Security.Claims;

namespace n_Tier_Architecture.PL.Areas.Admin.Controllers
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
        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm] ProductRequest request)
        {
            var result = await _productService.CreateProduct(request);
            return Ok(result);

        }
        [HttpGet("")]
        public async Task<ActionResult<List<ProductResponse>>> GetAllProductsAsync()
        {
            var products = await _productService.GelAllProductsAsync(Request);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var product = _productService.GetById(id);
            if (product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }
       


    }
}
