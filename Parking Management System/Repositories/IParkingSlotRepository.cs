using Parking_Management_System.Models;

namespace Parking_Management_System.Repositories
{
    public interface IParkingSlotRepository
    {
        Task<List<ParkingSlot>> GetAllAsync();

        Task<ParkingSlot?> GetByIdAsync(int id);

        Task<ParkingSlot?> GetBySlotNumberAsync(string slotNumber);

        Task<ParkingSlot> AddAsync(ParkingSlot parkingSlot);

        Task<bool> UpdateAsync(ParkingSlot parkingSlot);

        Task<bool> DeleteAsync(int id);
    }
}
