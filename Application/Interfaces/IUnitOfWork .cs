namespace Smart_Parking_System.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IParkingAreaRepository ParkingAreas { get; }
        IParkingSpotRepository ParkingSpots { get; }
        IReservationRepository Reservations { get; }
        Task<int> CompleteAsync();
    }
}
