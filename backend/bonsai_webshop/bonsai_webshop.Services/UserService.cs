using AutoMapper;
using bonsai_webshop.DataContext.Context;
using bonsai_webshop.DataContext.DTOs;
using bonsai_webshop.DataContext.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace bonsai_webshop.Services
{
    public interface IUserService
    {
        Task<TokenResponseDTO> LoginAsync(UserLoginDTO userLoginDto, HttpResponse response);
        Task<TokenResponseDTO> RefreshTokenAsync(HttpRequest request, HttpResponse response);
        Task LogoutAsync(HttpRequest request, HttpResponse response);
        Task<UserDTO> RegisterAsync(UserCreateDTO userCreateDto);
    }
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserService(AppDbContext context, IMapper mapper) { 
            _context = context;
            _mapper = mapper;
        }
        public async Task<TokenResponseDTO> LoginAsync(UserLoginDTO userLoginDto, HttpResponse response)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == userLoginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid email or password!");
            }

            var accessToken = await GenerateAccessToken(user);
            var refreshToken = await GenerateRefreshToken(user);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _context.SaveChangesAsync();

            response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                //Cross-site kérések miatt None a fejlesztés ideje alatt
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            return new TokenResponseDTO
            {
                AccessToken = accessToken
            };
        }
        public async Task<TokenResponseDTO> RefreshTokenAsync(HttpRequest request, HttpResponse response)
        {
            var refreshToken = request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken)) throw new UnauthorizedAccessException();

            //Dekódolni kell a +, = jelek miatt
            refreshToken = Uri.UnescapeDataString(refreshToken);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);

            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                throw new UnauthorizedAccessException();

            var newAccessToken = await GenerateAccessToken(user);
            var newRefreshToken = await GenerateRefreshToken(user);

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _context.SaveChangesAsync();

            response.Cookies.Append("refreshToken", newRefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                //Cross-site kérések miatt None a fejlesztés ideje alatt
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7),
            });

            return new TokenResponseDTO { AccessToken = newAccessToken };
        }
        private async Task<string> GenerateAccessToken(User user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("randomSztring12345_x2____randomSztring12345_x2"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(15);

            var id = await GetClaimsIdentity(user);
            var token = new JwtSecurityToken(
                issuer: "https://localhost:7024",
                audience: "https://localhost:7024",
                claims: id.Claims,
                expires: expires,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task LogoutAsync(HttpRequest request, HttpResponse response)
        {
            var refreshToken = request.Cookies["refreshToken"];
            if (!string.IsNullOrEmpty(refreshToken))
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
                if (user != null)
                {
                    user.RefreshToken = null;
                    user.RefreshTokenExpiryTime = null;
                    await _context.SaveChangesAsync();
                }
            }

            response.Cookies.Delete("refreshToken");
        }
        private async Task<string> GenerateRefreshToken(User user)
        {
            var randomBytes = new byte[64];
            using var randomGenerator = RandomNumberGenerator.Create();
            randomGenerator.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
        private async Task<ClaimsIdentity> GetClaimsIdentity(User user)
        {
            var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role.ToString()),
            new(ClaimTypes.Sid, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.AuthTime, DateTime.Now.ToString(CultureInfo.InvariantCulture))
        };

            return new ClaimsIdentity(claims, "Token");
        }

        public async Task<UserDTO> RegisterAsync(UserCreateDTO userCreateDto)
        {
            var emailAlreadyInUse = await _context.Users
                .AnyAsync(x => x.Email == userCreateDto.Email);
            if (emailAlreadyInUse)
            {
                throw new Exception($"The given email address is already in use.");
            }

            var user = _mapper.Map<User>(userCreateDto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password);            

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(user);
        }
    }
}
