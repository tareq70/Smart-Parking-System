namespace Smart_Parking_System.Application.Dtos.ParkingSpot
{
    public class ParkingSpotDto
    {
        public Guid Id { get; set; }
        public string SpotNumber { get; set; } = null!;
        public string ParkingAreaName { get; set; } = null!;
        public bool IsOccupied { get; set; }
        public bool IsReserved { get; set; }
    }
}
