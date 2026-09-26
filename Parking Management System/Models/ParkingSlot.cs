namespace Parking_Management_System.Models
{
    public class ParkingSlot
    {
        public int Id { get; set; }

        public string SlotNumber { get; set; } = string.Empty;

        public int Floor { get; set; }

        public string VehicleType { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }
    }
}
