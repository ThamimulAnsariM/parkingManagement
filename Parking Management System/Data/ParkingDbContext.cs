using Microsoft.EntityFrameworkCore;
using Parking_Management_System.Models;

namespace Parking_Management_System.Data
{
    public class ParkingDbContext : DbContext
    {
        public ParkingDbContext(DbContextOptions<ParkingDbContext> options)
        : base(options)
        {
        }
        public DbSet<ParkingSlot> ParkingSlots { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParkingSlot>()
                .HasIndex(x => x.SlotNumber)
                .IsUnique();
            modelBuilder.Entity<RefreshToken>()
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
