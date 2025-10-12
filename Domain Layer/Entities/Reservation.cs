using Smart_Parking_System.Application.Enums;

namespace Smart_Parking_System.DomainLayer.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }          

        public Guid ParkingSpotId { get; set; }   
        public ParkingSpot ParkingSpot { get; set; } = default!;

        public ReservationStatus Status { get; set; } = ReservationStatus.Active;

        public DateTime StartTimeUtc { get; set; }
        public DateTime EndTimeUtc { get; set; }

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public byte[]? RowVersion { get; set; }
    }
}
