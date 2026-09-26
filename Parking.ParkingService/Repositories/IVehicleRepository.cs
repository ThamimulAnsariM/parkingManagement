using Parking.ParkingService.Models;

namespace Parking.ParkingService.Repositories
{
    public interface IVehicleRepository
    {
        Task<List<Vehicle>> GetAllAsync();

        Task<Vehicle?> GetByIdAsync(int id);

        Task<Vehicle> CreateAsync(Vehicle vehicle);

        Task<bool> UpdateAsync(int id, Vehicle vehicle);

        Task<bool> DeleteAsync(int id);
    }
}
