using Microsoft.EntityFrameworkCore;
using Parking.ParkingService.Data;
using Parking.ParkingService.Models;

namespace Parking.ParkingService.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ParkingDbContext _context;

        public VehicleRepository(ParkingDbContext context)
        {
            _context = context;
        }

        public async Task<List<Vehicle>> GetAllAsync()
        {
            return await _context.Vehicles.ToListAsync();
        }

        public async Task<Vehicle?> GetByIdAsync(int id)
        {
            return await _context.Vehicles.FindAsync(id);
        }

        public async Task<Vehicle> CreateAsync(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return vehicle;
        }

        public async Task<bool> UpdateAsync(int id, Vehicle vehicle)
        {
            var existingVehicle = await _context.Vehicles.FindAsync(id);

            if (existingVehicle == null)
                return false;

            existingVehicle.VehicleNumber = vehicle.VehicleNumber;
            existingVehicle.VehicleType = vehicle.VehicleType;
            existingVehicle.OwnerName = vehicle.OwnerName;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
                return false;

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return true;
        }   
    }
}
