namespace Parking.ParkingService.Models
{
    public class ParkingRecord
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public int ParkingSlotId { get; set; }

        public DateTime EntryTime { get; set; }

        public DateTime? ExitTime { get; set; }

        public decimal? Amount { get; set; }

        public string Status { get; set; } = "Parked";

        // Navigation Properties
        public Vehicle Vehicle { get; set; } = null!;

        public ParkingSlot ParkingSlot { get; set; } = null!;
    }
}
