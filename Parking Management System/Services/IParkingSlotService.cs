using Parking_Management_System.DTOs;
using Parking_Management_System.Models;

namespace Parking_Management_System.Services
{
    public interface IParkingSlotService
    {
        Task<List<ParkingSlotDto>> GetAllAsync();

        Task<ParkingSlotDto?> GetByIdAsync(int id);

        Task<ParkingSlot> CreateAsync(CreateParkingSlotDto parkingSlot);

        Task<bool> UpdateAsync(int id, UpdateParkingSlotDto parkingSlot);

        Task<bool> DeleteAsync(int id);
    }
}
