namespace Smart_Parking_System.Application.Dtos.ParkingArea
{
    public class CreateParkingAreaDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
        public int TotalSpots { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
