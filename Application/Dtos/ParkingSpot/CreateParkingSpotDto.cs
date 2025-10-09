namespace Smart_Parking_System.Application.Dtos.ParkingSpot
{
    public class CreateParkingSpotDto
    {
        public Guid ParkingAreaId { get; set; }
        public string SpotNumber { get; set; } = null!;

    }
}
