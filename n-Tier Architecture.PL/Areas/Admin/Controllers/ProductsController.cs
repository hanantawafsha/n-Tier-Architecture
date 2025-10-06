using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTierArchitecture.BLL.Services.Classes;
using NTierArchitecture.BLL.Services.Interfaces;
using NTierArchitecture.DAL.DTO.Requests;
using NTierArchitecture.DAL.DTO.Responses;
using System.Security.Claims;

namespace NTierArchitecture.PL.Areas.Admin.Controllers
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
        public async Task<ActionResult<List<ProductResponse>>> GetAllProductsAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var products = await _productService.GelAllProductsAsync(Request,false, pageNumber, pageSize);
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
