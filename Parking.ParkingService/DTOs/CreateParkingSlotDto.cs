namespace Parking.ParkingService.DTOs
{
    public class CreateParkingSlotDto
    {
        public string SlotNumber { get; set; } = string.Empty;

        public string VehicleType { get; set; } = string.Empty;
    }
}
