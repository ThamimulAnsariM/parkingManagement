using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parking.ParkingService.DTOs;
using Parking.ParkingService.Services;

namespace Parking.ParkingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ParkingSlotsController : ControllerBase
    {
        private readonly IParkingSlotService _service;

        public ParkingSlotsController(
            IParkingSlotService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var slots = await _service.GetAllAsync();

            return Ok(slots);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var slot = await _service.GetByIdAsync(id);

            if (slot == null)
            {
                return NotFound(new
                {
                    message = "Parking slot not found."
                });
            }

            return Ok(slot);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateParkingSlotDto dto)
        {
            var slot = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = slot.Id },
                slot);
        }

        [Authorize(Roles ="Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateParkingSlotDto dto)
        {
            var updated = await _service.UpdateAsync(
                id,
                dto);

            if (!updated)
            {
                return NotFound(new
                {
                    message = "Parking slot not found."
                });
            }

            return Ok(new
            {
                message = "Parking slot updated successfully."
            });
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "Parking slot not found."
                });
            }

            return Ok(new
            {
                message = "Parking slot deleted successfully."
            });
        }
    }
}
