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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private int UserId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType<TokenResponseDTO>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userDto)
        {
            try
            {
                var tokenResponse = await _userService.LoginAsync(userDto, Response);
                return Ok(tokenResponse);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType<TokenResponseDTO>(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                var tokenResponse = await _userService.RefreshTokenAsync(Request, Response);
                return Ok(tokenResponse);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "Invalid or expired refresh token." });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync(Request, Response);
            return Ok(new { message = "Logged out successfully." });
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType<TokenResponseDTO>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] UserCreateDTO userCreateDto)
        {
            var result = await _userService.RegisterAsync(userCreateDto, Response);
            return Ok(result);
        }
    }
}
