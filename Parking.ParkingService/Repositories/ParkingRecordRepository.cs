using Microsoft.EntityFrameworkCore;
using Parking.ParkingService.Data;
using Parking.ParkingService.Models;

namespace Parking.ParkingService.Repositories
{
    public class ParkingRecordRepository: IParkingRecordRepository
    {
        private readonly ParkingDbContext _context;

        public ParkingRecordRepository(ParkingDbContext context)
        {
            _context = context;
        }

        public async Task<ParkingRecord> CreateAsync(
            ParkingRecord record)
        {
            _context.ParkingRecords.Add(record);

            await _context.SaveChangesAsync();

            return record;
        }

        public async Task<ParkingRecord?> GetByIdAsync(int id)
        {
            return await _context.ParkingRecords
                .Include(x => x.Vehicle)
                .Include(x => x.ParkingSlot)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ParkingRecord>> GetAllAsync()
        {
            return await _context.ParkingRecords
                .Include(x => x.Vehicle)
                .Include(x => x.ParkingSlot)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(
            ParkingRecord record)
        {
            _context.ParkingRecords.Update(record);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
