using Microsoft.EntityFrameworkCore;
using Parking_Management_System.DTOs;
using Parking_Management_System.Models;
using Parking_Management_System.Repositories;

namespace Parking_Management_System.Services
{
    public class ParkingSlotService : IParkingSlotService
    {

        public readonly IParkingSlotRepository _repository;

        public ParkingSlotService(IParkingSlotRepository repository) 
        {
            _repository = repository;
        }

        public async Task<ParkingSlot> CreateAsync(CreateParkingSlotDto dto)
        {
            // Business validation
           
            var existingSlot = await _repository.GetBySlotNumberAsync(dto.SlotNumber);

            if (existingSlot != null)
            {
                throw new ArgumentException(
                    $"Parking slot '{dto.SlotNumber}' already exists.");
            }

            var parkingSlot = new ParkingSlot
            {
                SlotNumber = dto.SlotNumber,
                Floor = dto.Floor,
                VehicleType = dto.VehicleType,
                IsAvailable = dto.IsAvailable
            };

            return await _repository.AddAsync(parkingSlot);

        }

        public async  Task<bool> DeleteAsync(int id)
        {
            var existingSlot = await _repository.GetByIdAsync(id);

            if (existingSlot == null)
            {
                return false;
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<List<ParkingSlotDto>> GetAllAsync()
        {
            var slots = await _repository.GetAllAsync();

            return slots.Select(slot => new ParkingSlotDto
            {
                Id = slot.Id,
                SlotNumber = slot.SlotNumber,
                Floor = slot.Floor,
                VehicleType = slot.VehicleType,
                IsAvailable = slot.IsAvailable
            }).ToList();
            
        }

        public async Task<ParkingSlotDto?> GetByIdAsync(int id)
        {
            var slot = await _repository.GetByIdAsync(id);

            if (slot == null)
            {
                return null;
            }

            return new ParkingSlotDto
            {
                Id = slot.Id,
                SlotNumber = slot.SlotNumber,
                Floor = slot.Floor,
                VehicleType = slot.VehicleType,
                IsAvailable = slot.IsAvailable
            };
            
        }

        public async Task<bool> UpdateAsync(int id, UpdateParkingSlotDto dto)
        {
          
            var existingSlot = await _repository.GetByIdAsync(id);

            if (existingSlot == null)
            {
                return false;
            }

            var duplicateSlot = await _repository
                .GetBySlotNumberAsync(dto.SlotNumber);

            if (duplicateSlot != null &&
                duplicateSlot.Id != id)
            {
                throw new ArgumentException(
                    $"Parking slot '{dto.SlotNumber}' already exists.");
            }

            existingSlot.SlotNumber = dto.SlotNumber;
            existingSlot.Floor = dto.Floor;
            existingSlot.VehicleType = dto.VehicleType;
            existingSlot.IsAvailable = dto.IsAvailable;

            return await _repository.UpdateAsync(existingSlot);
        }
    }
}
