using Parking.ParkingService.Models;

namespace Parking.ParkingService.Repositories
{
    public interface IParkingSlotRepository
    {
        Task<List<ParkingSlot>> GetAllAsync();

        Task<ParkingSlot?> GetByIdAsync(int id);

        Task<ParkingSlot> AddAsync(ParkingSlot parkingSlot);

        Task<bool> UpdateAsync(ParkingSlot parkingSlot);

        Task<bool> DeleteAsync(int id);
    }
}
