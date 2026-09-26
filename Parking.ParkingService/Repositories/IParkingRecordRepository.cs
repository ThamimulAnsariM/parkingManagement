using Parking.ParkingService.Models;

namespace Parking.ParkingService.Repositories
{
    public interface IParkingRecordRepository
    {
        Task<ParkingRecord> CreateAsync(ParkingRecord record);

        Task<ParkingRecord?> GetByIdAsync(int id);

        Task<List<ParkingRecord>> GetAllAsync();

        Task<bool> UpdateAsync(ParkingRecord record);
    }
}
