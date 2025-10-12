using Smart_Parking_System.Application.Enums;

namespace Smart_Parking_System.Application.Dtos.Reservation
{
    public class CreateReservationDto
    {

        public Guid ParkingSpotId { get; set; }
        public DateTime StartTimeUtc { get; set; }
        public DateTime EndTimeUtc { get; set; }


    }
}
