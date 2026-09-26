using Parking.ParkingService.DTOs;
using Parking.ParkingService.Models;
using Parking.ParkingService.Repositories;

namespace Parking.ParkingService.Services
{
    public class ParkingSlotService : IParkingSlotService
    {
        private readonly IParkingSlotRepository _repository;

        public ParkingSlotService(
            IParkingSlotRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ParkingSlotResponseDto>> GetAllAsync()
        {
            var slots = await _repository.GetAllAsync();

            return slots.Select(x => new ParkingSlotResponseDto
            {
                Id = x.Id,
                SlotNumber = x.SlotNumber,
                VehicleType = x.VehicleType,
                IsAvailable = x.IsAvailable
            }).ToList();
        }

        public async Task<ParkingSlotResponseDto?> GetByIdAsync(int id)
        {
            var slot = await _repository.GetByIdAsync(id);

            if (slot == null)
                return null;

            return new ParkingSlotResponseDto
            {
                Id = slot.Id,
                SlotNumber = slot.SlotNumber,
                VehicleType = slot.VehicleType,
                IsAvailable = slot.IsAvailable
            };
        }

        public async Task<ParkingSlotResponseDto> CreateAsync(
            CreateParkingSlotDto dto)
        {
            var parkingSlot = new ParkingSlot
            {
                SlotNumber = dto.SlotNumber,
                VehicleType = dto.VehicleType,
                IsAvailable = true
            };

            var created = await _repository.AddAsync(parkingSlot);

            return new ParkingSlotResponseDto
            {
                Id = created.Id,
                SlotNumber = created.SlotNumber,
                VehicleType = created.VehicleType,
                IsAvailable = created.IsAvailable
            };
        }

        public async Task<bool> UpdateAsync(
            int id,
            UpdateParkingSlotDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);

            if (existing == null)
                return false;

            existing.SlotNumber = dto.SlotNumber;
            existing.VehicleType = dto.VehicleType;
            existing.IsAvailable = dto.IsAvailable;

            return await _repository.UpdateAsync(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
