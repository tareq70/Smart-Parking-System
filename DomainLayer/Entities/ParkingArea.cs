namespace Smart_Parking_System.DomainLayer.Entities
{
    public class ParkingArea
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
        public int TotalSpots { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<ParkingSpot> Spots { get; set; } = new List<ParkingSpot>();


    }
}
