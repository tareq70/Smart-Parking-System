namespace Smart_Parking_System.DomainLayer.Entities
{
    public class ParkingSpot
    {
        public Guid Id { get; set; }              
        public string SpotNumber { get; set; } = default!;
        public bool IsOccupied { get; set; } = false;
        public bool IsReserved { get; set; } = false;

        public ParkingArea ParkingArea { get; set; } = default!;
        public Guid ParkingAreaId { get; set; } 


    }
}
