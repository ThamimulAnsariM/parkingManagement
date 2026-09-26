namespace Parking.ParkingService.DTOs
{
    public class UpdateParkingSlotDto
    {
        public string SlotNumber { get; set; } = string.Empty;

        public string VehicleType { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }
    }
}
