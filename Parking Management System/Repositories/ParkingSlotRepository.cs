using Microsoft.EntityFrameworkCore;
using Parking_Management_System.Data;
using Parking_Management_System.Models;

namespace Parking_Management_System.Repositories
{
    public class ParkingSlotRepository : IParkingSlotRepository
    {

        private readonly ParkingDbContext _context;

        public ParkingSlotRepository(ParkingDbContext context)
        {
            _context = context;
        }
        public async Task<ParkingSlot> AddAsync(ParkingSlot parkingSlot)
        {
            await _context.ParkingSlots.AddAsync(parkingSlot);
            await _context.SaveChangesAsync();

            return parkingSlot;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var parkingSlot = await _context.ParkingSlots
            .FirstOrDefaultAsync(x => x.Id == id);

            if (parkingSlot == null)
            {
                return false;
            }

            _context.ParkingSlots.Remove(parkingSlot);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<ParkingSlot>> GetAllAsync()
        {
            return await _context.ParkingSlots.ToListAsync();
        }

        public async Task<ParkingSlot?> GetByIdAsync(int id)
        {
            return await _context.ParkingSlots
            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ParkingSlot?> GetBySlotNumberAsync(string slotNumber)
        {
            return await _context.ParkingSlots
        .FirstOrDefaultAsync(x => x.SlotNumber == slotNumber);
        }

        public async Task<bool> UpdateAsync(ParkingSlot parkingSlot)
        {
            var existingSlot = await _context.ParkingSlots
            .FirstOrDefaultAsync(x => x.Id == parkingSlot.Id);

            if (existingSlot == null)
            {
                return false;
            }

            existingSlot.SlotNumber = parkingSlot.SlotNumber;
            existingSlot.Floor = parkingSlot.Floor;
            existingSlot.VehicleType = parkingSlot.VehicleType;
            existingSlot.IsAvailable = parkingSlot.IsAvailable;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
