using Microsoft.EntityFrameworkCore;
using Parking.ParkingService.Data;
using Parking.ParkingService.Models;

namespace Parking.ParkingService.Repositories
{
    public class ParkingSlotRepository : IParkingSlotRepository
    {
        private readonly ParkingDbContext _context;

        public ParkingSlotRepository(ParkingDbContext context)
        {
            _context = context;
        }

        public async Task<List<ParkingSlot>> GetAllAsync()
        {
            return await _context.ParkingSlots
                .ToListAsync();
        }

        public async Task<ParkingSlot?> GetByIdAsync(int id)
        {
            return await _context.ParkingSlots
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ParkingSlot> AddAsync(
            ParkingSlot parkingSlot)
        {
            _context.ParkingSlots.Add(parkingSlot);

            await _context.SaveChangesAsync();

            return parkingSlot;
        }

        public async Task<bool> UpdateAsync(
            ParkingSlot parkingSlot)
        {
            _context.ParkingSlots.Update(parkingSlot);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var parkingSlot = await GetByIdAsync(id);

            if (parkingSlot == null)
            {
                return false;
            }

            _context.ParkingSlots.Remove(parkingSlot);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
