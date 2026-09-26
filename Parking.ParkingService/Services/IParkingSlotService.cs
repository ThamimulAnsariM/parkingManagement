using Parking.ParkingService.DTOs;

namespace Parking.ParkingService.Services
{
    public interface IParkingSlotService
    {
        Task<List<ParkingSlotResponseDto>> GetAllAsync();

        Task<ParkingSlotResponseDto?> GetByIdAsync(int id);

        Task<ParkingSlotResponseDto> CreateAsync(
            CreateParkingSlotDto dto);

        Task<bool> UpdateAsync(
            int id,
            UpdateParkingSlotDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
