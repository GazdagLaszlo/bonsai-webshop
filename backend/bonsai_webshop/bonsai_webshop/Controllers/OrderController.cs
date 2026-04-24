using bonsai_webshop.DataContext.DTOs;
using bonsai_webshop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bonsai_webshop.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private int _userId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromBody] OrderCreateDTO createDto)
        {
            await _orderService.CreateAsync(createDto, _userId);
            return Ok();
        }
    }
}
