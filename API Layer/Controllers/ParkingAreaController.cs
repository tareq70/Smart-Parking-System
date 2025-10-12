using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart_Parking_System.Application.Dtos.ParkingArea;
using Smart_Parking_System.Application.Interfaces;
using Smart_Parking_System.DomainLayer.Entities;

namespace Smart_Parking_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingAreaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ParkingAreaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllParkingAreas")]
        public async Task<IActionResult> GetAllParkingAreas()
        {
            var areas = await _unitOfWork.ParkingAreas.GetAllAsync();
            return Ok(areas);
        }
        [HttpGet("GetParkingAreaById")]
        public async Task<IActionResult> GetParkingAreaById(Guid id)
        {
            var area= await _unitOfWork.ParkingAreas.GetByIdAsync(id);
            if (area == null)
                return NotFound("Parking area not found.");
            return Ok(area);
        }
        [HttpGet("GetParkingAreaByName")]
        public async Task<IActionResult> GetParkingAreaByName(string name)
        {
            var area = await _unitOfWork.ParkingAreas.GetByNameAsync(name);
            if (area == null)
                return NotFound("Parking area not found.");
            return Ok(area);
        }
        [HttpPost("CreateParkingArea")]
        public async Task<IActionResult> CreateParkingArea(CreateParkingAreaDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var area = new ParkingArea
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Address = dto.Address,
                TotalSpots = dto.TotalSpots,
                CreatedAt = DateTime.Now
            };

            await _unitOfWork.ParkingAreas.AddAsync(area);
            await _unitOfWork.CompleteAsync();

            return Ok(area);
        }

        [HttpPut("UpdateParkingArea")]
        public async Task<IActionResult> UpdateParkingArea(Guid id, UpdateParkingAreaDto dto)
        {
            var area = await _unitOfWork.ParkingAreas.GetByIdAsync(id);
            if (area == null)
                return NotFound("Parking area not found.");
            area.Name = dto.Name;
            area.Address = dto.Address;
            area.TotalSpots = dto.TotalSpots;
            _unitOfWork.ParkingAreas.Update(area);
            await _unitOfWork.CompleteAsync();
            return Ok(area);
        }


        [HttpDelete("DeleteParkingArea")]
        public async Task<IActionResult> DeleteParkingArea(Guid id)
        {
            var area = await _unitOfWork.ParkingAreas.GetByIdAsync(id);
            if (area == null)
                return NotFound("Parking area not found.");
            _unitOfWork.ParkingAreas.Delete(area);
            await _unitOfWork.CompleteAsync();
            return Ok("Parking area deleted successfully.");
        }


    }
}
