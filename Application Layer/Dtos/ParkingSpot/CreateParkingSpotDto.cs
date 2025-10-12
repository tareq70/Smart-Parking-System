namespace Smart_Parking_System.Application.Dtos.ParkingSpot
{
    public class CreateParkingSpotDto
    {
        public string SpotNumber { get; set; } = null!;

        public Guid ParkingAreaId { get; set; }

    }
}
