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

        [HttpGet]
        public async Task<IActionResult> GetAllParkingAreas()
        {
            var areas = await _unitOfWork.ParkingAreas.GetAllAsync();
            return Ok(areas);
        }

              
        [HttpPost]
        public async Task<IActionResult> AddArea(CreateParkingAreaDto dto)
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
    }
}
