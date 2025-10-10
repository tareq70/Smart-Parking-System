using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smart_Parking_System.Application.Dtos.Reservation;
using Smart_Parking_System.Application.Enums;
using Smart_Parking_System.Application.Interfaces;
using Smart_Parking_System.DomainLayer.Entities;
using Smart_Parking_System.Infrastructure.Data;
using System.Data;
using System.Security.Claims;

namespace Smart_Parking_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReservationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllActiveReservationsByAreaId")]
        public async Task<IActionResult> GetAllActiveReservations(Guid areaId)
        {
            var reservations = await _unitOfWork.Reservations.GetActiveReservationsByAreaAsync(areaId);
            return Ok(reservations);
        }

        //User ID from token---------------

        [Authorize]
        [HttpPost("CreateReservation")]
        public async Task<IActionResult> CreateReservation(CreateReservationDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var reservation = await _unitOfWork.Reservations.CreateReservationAsync(Guid.Parse(userId), dto);
            return Ok(reservation);
        }

        [Authorize]
        [HttpGet("GetMyReservations")]
        public async Task<IActionResult> GetMyReservations()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();
            var reservations = await _unitOfWork.Reservations.GetByUserIdAsync(Guid.Parse(userId));
            return Ok(reservations);
        }

        [HttpGet("GetReservationById")]
        public async Task<IActionResult> GetReservationById(Guid reservationId)
        {
            var reservation = await _unitOfWork.Reservations.GetByIdAsync(reservationId);
            if (reservation == null)
                return NotFound();
            return Ok(reservation);
        }

        [HttpGet("CancelReservation")]
        public async Task<IActionResult> CancelReservation(Guid reservationId)
        {
            var reservation = await _unitOfWork.Reservations.GetByIdAsync(reservationId);
            if (reservation == null)
                return NotFound();
            if (reservation.Status != ReservationStatus.Active)
                return BadRequest("Only active reservations can be cancelled.");
            reservation.Status = ReservationStatus.Cancelled;
            await _unitOfWork.CompleteAsync();
            return Ok(reservation);
        }

    }




}
