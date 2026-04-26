using bonsai_webshop.DataContext.DTOs;
using bonsai_webshop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.RateLimiting;

namespace bonsai_webshop.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private string? userId => User.FindFirstValue(ClaimTypes.NameIdentifier);
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
            catch (Exception)
            {
                return StatusCode(500, new { message = "Server error" });
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
                return Unauthorized(new { message = "Invalid or expired refresh token" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Server error" });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _userService.LogoutAsync(Request, Response);
                return Ok(new { message = "Logged out successfully" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Server error" });
            }
        }

        [EnableRateLimiting("register")]
        [HttpPost]
        [AllowAnonymous]        
        [ProducesResponseType<TokenResponseDTO>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] UserCreateDTO userCreateDto)
        {
            try
            {
                var result = await _userService.RegisterAsync(userCreateDto, Response);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Server error" });
            }
        }

        [HttpPut]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO dto)
        {            
            if (userId == null)
                return Unauthorized(new { message = "Unauthorized" });

            try
            {
                await _userService.ChangePassword(int.Parse(userId), dto);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Server error" });
            }
        }

        [HttpPut]
        public async Task<ActionResult<UserDTO>> UpdateOwnProfile([FromBody] UserUpdateDTO dto)
        {
            if (userId == null)
                return Unauthorized(new { message = "Unauthorized" });

            try
            {
                var result = await _userService.UpdateOwnProfile(int.Parse(userId), dto);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Server error" });
            }
        }
    }
}
