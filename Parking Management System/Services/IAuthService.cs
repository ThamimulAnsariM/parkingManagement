using Parking_Management_System.DTOs;

namespace Parking_Management_System.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginDto dto);

        Task<LoginResponseDto?> RefreshTokenAsync(string refershToken);
    }
}
