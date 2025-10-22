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
        [Authorize("Admin")]
        public async Task<IActionResult> GetAllActiveReservations(Guid areaId)
        {
            var reservations = await _unitOfWork.Reservations.GetActiveReservationsByAreaAsync(areaId);
            return Ok(reservations);
        }


        [HttpGet("GetReservationBySpotId")]
        [Authorize("Admin")]
        public async Task<IActionResult> GetReservationBySpotId(Guid spotId)
        {
            var reservation = await _unitOfWork.Reservations.GetActiveReservationBySpotIdAsync(spotId);
            if (reservation == null)
                return NotFound();
            return Ok(reservation);
        }


        [HttpGet("GetReservationById")]
        public async Task<IActionResult> GetReservationById(Guid reservationId)
        {
            var reservation = await _unitOfWork.Reservations.GetByIdAsync(reservationId);
            if (reservation == null)
                return NotFound();
            return Ok(reservation);
        }


        [Authorize]
        [HttpPost("CreateReservation")]
        public async Task<IActionResult> CreateReservation(CreateReservationDto dto)
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(claims));

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


        [Authorize]
        [HttpPut("CancelReservation")]
        public async Task<IActionResult> CancelReservation(Guid reservationId)
        {
            //var reservation = await _unitOfWork.Reservations.GetByIdAsync(reservationId);
            //if (reservation == null)
            //    return NotFound();
            //if (reservation.Status != ReservationStatus.Active)
            //    return BadRequest("Only active reservations can be cancelled.");
            //reservation.Status = ReservationStatus.Cancelled;
            //await _unitOfWork.CompleteAsync();
            //return Ok(reservation);

            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (UserId == null)
                return Unauthorized();

            var reservation = await _unitOfWork.Reservations.GetByIdAsync(reservationId);
            if (reservation == null)
                return NotFound("Reservation not found");

            if (reservation.UserId != Guid.Parse(UserId))
                return Forbid("You can only cancel your own reservations.");

            if (reservation.Status != ReservationStatus.Active)
                return BadRequest("Only active reservations can be cancelled.");

            reservation.Status = ReservationStatus.Cancelled;

            var spot = await _unitOfWork.ParkingSpots.GetByIdAsync(reservation.ParkingSpotId);
            if (spot != null)
            {
                spot.IsOccupied = false;
                _unitOfWork.ParkingSpots.Update(spot);

            }

            _unitOfWork.Reservations.Update(reservation);

            await _unitOfWork.CompleteAsync();
            return Ok(reservation);


        }


        [Authorize]
        [HttpGet("GetActiveReservation")]
        public async Task<IActionResult> GetActiveReservation()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null)
                return Unauthorized("User not authenticated.");

            var userId = Guid.Parse(userIdString);
            var reservation = await _unitOfWork.Reservations.GetActiveReservationByUserIdAsync(userId);
            if (reservation == null)
                return NotFound("No active reservation found for this user.");
            return Ok(reservation);
        }


        [Authorize]
        [HttpGet("GetUserReservationHistory")]
        public async Task<IActionResult> GetUserReservationHistory()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null)
                return Unauthorized("User not authenticated.");

            var userId = Guid.Parse(userIdString);

            var reservations = await _unitOfWork.Reservations.GetByUserIdAsync(userId);
            if (reservations == null || !reservations.Any() || reservations.Count() == 0)
                return NotFound("No reservation history found for this user.");

            var History_Reservation = reservations.Where(r => r.Status == ReservationStatus.Completed || r.Status == ReservationStatus.Cancelled);
            return Ok(History_Reservation);
        }


    }
}
