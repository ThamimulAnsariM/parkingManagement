using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Parking.AuthService.Data;
using Parking.AuthService.DTOs;
using Parking.AuthService.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
namespace Parking.AuthService.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(
            AuthDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<LoginResponseDto?> LoginAsync(
            LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.Username == dto.Username);

            if (user == null)
            {
                return null;
            }

            var passwordResult =
                _passwordHasher.VerifyHashedPassword(
                    user,
                    user.Password,
                    dto.Password);

            if (passwordResult ==
                PasswordVerificationResult.Failed)
            {
                return null;
            }

            return await GenerateTokensAsync(user);
        }

        public async Task<bool> RegisterAsync(
    LoginDto dto,
    string role)
        {
            var exists = await _context.Users
                .AnyAsync(x => x.Username == dto.Username);

            if (exists)
            {
                return false;
            }

            var user = new User
            {
                Username = dto.Username,
                Role = role
            };

            user.Password = _passwordHasher.HashPassword(
                user,
                dto.Password);

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<LoginResponseDto?> RefreshTokenAsync(
            string refreshToken)
        {
            var storedToken = await _context.RefreshTokens
                .Include(x => x.User)
                .FirstOrDefaultAsync(x =>
                    x.Token == refreshToken);

            if (storedToken == null)
            {
                return null;
            }

            if (storedToken.IsRevoked)
            {
                return null;
            }

            if (storedToken.ExpiresAt <= DateTime.UtcNow)
            {
                return null;
            }

            var user = storedToken.User;

            // Revoke old refresh token
            storedToken.IsRevoked = true;

            return await GenerateTokensAsync(user);
        }

        private async Task<LoginResponseDto>
            GenerateTokensAsync(User user)
        {
            var claims = new[]
            {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id.ToString()),

            new Claim(
                ClaimTypes.Name,
                user.Username),

            new Claim(
                ClaimTypes.Role,
                user.Role)
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var expiryMinutes = double.Parse(
                _configuration["Jwt:ExpiryMinutes"]!);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    expiryMinutes),
                signingCredentials: credentials);

            var accessToken =
                new JwtSecurityTokenHandler()
                    .WriteToken(token);

            var refreshToken =
                Convert.ToBase64String(
                    RandomNumberGenerator.GetBytes(64));

            var refreshTokenDays = double.Parse(
                _configuration["Jwt:RefreshTokenDays"]!);

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,

                ExpiresAt = DateTime.UtcNow.AddDays(
                    refreshTokenDays),

                IsRevoked = false,

                UserId = user.Id
            };

            _context.RefreshTokens.Add(
                refreshTokenEntity);

            await _context.SaveChangesAsync();

            return new LoginResponseDto
            {
                AccessToken = accessToken,

                RefreshToken = refreshToken,

                Username = user.Username,

                Role = user.Role,

                ExpiresIn = (int)(expiryMinutes * 60)
            };
        }
    }
}
