using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart_Parking_System.Application.Dtos.ParkingSpot;
using Smart_Parking_System.Application.Interfaces;
using Smart_Parking_System.DomainLayer.Entities;

namespace Smart_Parking_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingSpotsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ParkingSpotsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetSpotsByAreaId")]
        public async Task<IActionResult> GetSpotsByAreaId(Guid areaId)
        {
            var spots = await _unitOfWork.ParkingSpots.GetSpotsByAreaIdAsync(areaId);
            if (spots == null || !spots.Any())
                return NotFound("No parking spots found for the specified area.");
            return Ok(spots);
        }
        [HttpGet("GetSpotById")]
        public async Task<IActionResult> GetSpotById(Guid spotId)
        {
            var spot = await _unitOfWork.ParkingSpots.GetByIdAsync(spotId);
            if (spot == null)
                return NotFound("Parking spot not found.");
            return Ok(spot);
        }


        [HttpPost("AddSpot")]
        public async Task<IActionResult> AddSpot(CreateParkingSpotDto createParkingSpotDto)
        {
            if (createParkingSpotDto == null || string.IsNullOrWhiteSpace(createParkingSpotDto.SpotNumber))
                return BadRequest("Invalid parking spot data.");
            var newSpot = new ParkingSpot
            {
                Id = Guid.NewGuid(),
                SpotNumber = createParkingSpotDto.SpotNumber,
                IsOccupied = false,
                IsReserved = false,
                ParkingAreaId = createParkingSpotDto.ParkingAreaId
            };
            await _unitOfWork.ParkingSpots.AddAsync(newSpot);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetSpotsByAreaId), new { areaId = newSpot.ParkingAreaId }, newSpot);

        }
        [HttpPut("UpdateSpot")]
        public async Task<IActionResult> UpdateSpot(Guid spotId, CreateParkingSpotDto updateParkingSpotDto)
        {
            if (updateParkingSpotDto == null || string.IsNullOrWhiteSpace(updateParkingSpotDto.SpotNumber))
                return BadRequest("Invalid parking spot data.");
            var existingSpot = await _unitOfWork.ParkingSpots.GetByIdAsync(spotId);
            if (existingSpot == null)
                return NotFound("Parking spot not found.");
            existingSpot.SpotNumber = updateParkingSpotDto.SpotNumber;
            existingSpot.ParkingAreaId = updateParkingSpotDto.ParkingAreaId;
            _unitOfWork.ParkingSpots.Update(existingSpot);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
        [HttpDelete("DeleteSpot")]
        public async Task<IActionResult> DeleteSpot(Guid spotId)
        {
            var spot = await _unitOfWork.ParkingSpots.GetByIdAsync(spotId);
            if (spot == null)
                return NotFound("Parking spot not found.");
            _unitOfWork.ParkingSpots.Delete(spot);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

    }
}
