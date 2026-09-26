namespace Parking_Management_System.DTOs
{
    public class ParkingSlotDto
    {
        public int Id { get; set; }

        public string SlotNumber { get; set; } = string.Empty;

        public int Floor { get; set; }

        public string VehicleType { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }
    }
}
