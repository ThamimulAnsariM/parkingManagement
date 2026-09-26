using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parking.AuthService.DTOs;
using Parking.AuthService.Services;

namespace Parking.AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new
            {
                message = "You are authenticated!"
            });
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
        public async Task<IActionResult> Refresh(
            RefreshTokenDto dto)
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
        [HttpPost("register")]
        public async Task<IActionResult> Register(
    LoginDto dto)
        {
            var result = await _authService.RegisterAsync(
                dto,
                "User");

            if (!result)
            {
                return BadRequest(new
                {
                    message = "Username already exists."
                });
            }

            return Ok(new
            {
                message = "User created successfully."
            });
        }
    }
}
