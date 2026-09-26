using Parking.AuthService.DTOs;

namespace Parking.AuthService.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginDto dto);

        Task<LoginResponseDto?> RefreshTokenAsync(string refreshToken);
        Task<bool> RegisterAsync(LoginDto dto, string role);
    }
}
