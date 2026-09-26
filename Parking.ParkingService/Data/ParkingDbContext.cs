using Microsoft.EntityFrameworkCore;
using Parking.ParkingService.Models;
using System.Collections.Generic;

namespace Parking.ParkingService.Data
{
    public class ParkingDbContext : DbContext
    {
        public ParkingDbContext(
       DbContextOptions<ParkingDbContext> options)
       : base(options)
        {
        }

        public DbSet<ParkingSlot> ParkingSlots { get; set; } = null;
        public DbSet<Vehicle> Vehicles { get; set; } = null;
        public DbSet<ParkingRecord> ParkingRecords { get; set; } = null!;
    }
}
