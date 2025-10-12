using Smart_Parking_System.Application.Enums;

namespace Smart_Parking_System.Application.Dtos.Reservation
{
    public class ReservationDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ParkingSpotId { get; set; }
        public DateTime StartTimeUtc { get; set; }
        public DateTime EndTimeUtc { get; set; }
        public ReservationStatus Status { get; set; } 
    }
}
