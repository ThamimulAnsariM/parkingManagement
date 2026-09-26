namespace Parking.ParkingService.Models
{
    public class ParkingSlot
    {
        public int Id { get; set; }

        public string SlotNumber { get; set; } = string.Empty;

        public string VehicleType { get; set; } = string.Empty;

        public bool IsAvailable { get; set; } = true;
    }
}
