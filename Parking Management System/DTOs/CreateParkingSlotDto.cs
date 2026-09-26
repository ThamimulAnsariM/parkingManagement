using System.ComponentModel.DataAnnotations;

namespace Parking_Management_System.DTOs
{
    public class CreateParkingSlotDto
    {
        [Required(ErrorMessage ="Slot number is required.")]
        [StringLength(20, ErrorMessage ="Slot number cannot exceed 20 charcters.")]
        public string SlotNumber { get; set; } = string.Empty;


        [Range(1, 100, ErrorMessage = "Floor must be between 1 and 100.")]
        public int Floor { get; set; }

        [Required(ErrorMessage = "Vehicle type is required.")]
        [StringLength(20, ErrorMessage = "Vehicle type cannot exceed 20 characters.")]
        public string VehicleType { get; set; } = string.Empty;


        public bool IsAvailable { get; set; }
    }
}
