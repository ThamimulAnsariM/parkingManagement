using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Parking_Management_System.DTOs;
using Parking_Management_System.Models;
using Parking_Management_System.Services;

namespace Parking_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);

            if (result == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid username or password."
                });
            }

            return Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenDto dto)
        {
            var result =
                await _authService.RefreshTokenAsync(
                    dto.RefreshToken);

            if (result == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid or expired refresh token."
                });
            }

            return Ok(result);
        }


        [HttpGet("generate-password")]
        public IActionResult GeneratePassword(string password)
        {
            var hasher = new PasswordHasher<User>();

            var user = new User();

            var hash = hasher.HashPassword(user, password);

            return Ok(hash);
        }
    }
}
