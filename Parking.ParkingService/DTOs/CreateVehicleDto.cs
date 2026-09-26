namespace Parking.ParkingService.DTOs
{
    public class CreateVehicleDto
    {
        public string VehicleNumber { get; set; } = string.Empty;

        public string VehicleType { get; set; } = string.Empty;

        public string OwnerName { get; set; } = string.Empty;
    }
}
