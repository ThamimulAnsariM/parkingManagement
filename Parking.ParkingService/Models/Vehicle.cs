namespace Parking.ParkingService.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        public string VehicleNumber { get; set; } = string.Empty;

        public string VehicleType { get; set; } = string.Empty;

        public string OwnerName { get; set; } = string.Empty;
    }
}
