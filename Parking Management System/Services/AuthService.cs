using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Parking_Management_System.Data;
using Parking_Management_System.DTOs;
using Parking_Management_System.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Parking_Management_System.Services
{
    public class AuthService : IAuthService
    {
        private readonly ParkingDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(ParkingDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginDto dto)
        {
            // var user = await _context.Users
            //.FirstOrDefaultAsync(x =>
            //    x.Username == dto.Username &&
            //    x.Password == dto.Password);
            var user = await _context.Users
             .FirstOrDefaultAsync(x => x.Username == dto.Username);

            if (user == null)
            {
                return null;
            }

            var passwordResult = _passwordHasher.VerifyHashedPassword(
                user,
                user.Password,
                dto.Password);

            if (passwordResult == PasswordVerificationResult.Failed)
            {
                return null;
            }

            if (user == null)
            {
                return null;
            }

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

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    double.Parse(
                        _configuration["Jwt:ExpiryMinutes"]!)),
                signingCredentials: credentials);

            var accessToken = new JwtSecurityTokenHandler()
            .WriteToken(token);

            var refreshToken = Convert.ToBase64String(
            RandomNumberGenerator.GetBytes(64));

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,

                ExpiresAt = DateTime.UtcNow.AddDays(
            double.Parse(
                _configuration["Jwt:RefreshTokenDays"]!)),

                IsRevoked = false,

                UserId = user.Id
            };

            _context.RefreshTokens.Add(refreshTokenEntity);

            await _context.SaveChangesAsync();

            return new LoginResponseDto
            {
                AccessToken = accessToken,

                RefreshToken = refreshToken,

                Username = user.Username,

                Role = user.Role,

                ExpiresIn = int.Parse(
            _configuration["Jwt:ExpiryMinutes"]!) * 60
            };

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

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    double.Parse(
                        _configuration["Jwt:ExpiryMinutes"]!)),
                signingCredentials: credentials);

            var accessToken = new JwtSecurityTokenHandler()
                .WriteToken(token);

            // Revoke old refresh token
            storedToken.IsRevoked = true;

            // Generate new refresh token
            var newRefreshToken =
                Convert.ToBase64String(
                    RandomNumberGenerator.GetBytes(64));

            var newRefreshTokenEntity = new RefreshToken
            {
                Token = newRefreshToken,

                ExpiresAt = DateTime.UtcNow.AddDays(
                    double.Parse(
                        _configuration["Jwt:RefreshTokenDays"]!)),

                IsRevoked = false,

                UserId = user.Id
            };

            _context.RefreshTokens.Add(
                newRefreshTokenEntity);

            await _context.SaveChangesAsync();

            return new LoginResponseDto
            {
                AccessToken = accessToken,

                RefreshToken = newRefreshToken,

                Username = user.Username,

                Role = user.Role,

                ExpiresIn = int.Parse(
                    _configuration["Jwt:ExpiryMinutes"]!) * 60
            };
        }
    }
}
