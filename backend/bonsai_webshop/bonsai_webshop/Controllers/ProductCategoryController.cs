using bonsai_webshop.DataContext.DTOs;
using bonsai_webshop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bonsai_webshop.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;
        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<ProductCategoryDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _productCategoryService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{urlSlug}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ProductCategoryDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByUrlSlugAsync(string urlSlug)
        {
            var result = await _productCategoryService.GetByUrlSlugAsync(urlSlug);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
