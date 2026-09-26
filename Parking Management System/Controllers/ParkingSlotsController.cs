using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parking_Management_System.DTOs;
using Parking_Management_System.Models;
using Parking_Management_System.Services;

namespace Parking_Management_System.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ParkingSlotsController : ControllerBase
    {

        private readonly IParkingSlotService _service;

        public ParkingSlotsController(IParkingSlotService service)
        {
            _service = service;
        }

        // GET: api/ParkingSlots
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var slots = await _service.GetAllAsync();

            return Ok(slots);
        }

        // GET: api/ParkingSlots/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var slot = await _service.GetByIdAsync(id);

            if (slot == null)
            {
                return NotFound();
            }

            return Ok(slot);
        }

        // POST: api/ParkingSlots
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create(CreateParkingSlotDto dto)
        {
                var createdSlot = await _service.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = createdSlot.Id },
                    createdSlot);
            
        }

        // PUT: api/ParkingSlots/1
        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Update(int id, UpdateParkingSlotDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }


        // DELETE: api/ParkingSlots/1
        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
