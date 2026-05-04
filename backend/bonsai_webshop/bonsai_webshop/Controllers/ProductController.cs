using bonsai_webshop.DataContext.DTOs;
using bonsai_webshop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bonsai_webshop.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService) {
            _productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<ProductDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync([FromQuery] string? urlSlug = null)
        {
            var result = await _productService.GetAllAsync(urlSlug);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _productService.GetByIdAsync(id);
            return Ok(result);
        }
    }
}
