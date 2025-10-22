using Smart_Parking_System.Application_Layer.Interfaces;

namespace Smart_Parking_System.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IParkingAreaRepository ParkingAreas { get; }
        IParkingSpotRepository ParkingSpots { get; }
        IReservationRepository Reservations { get; }
        IAuthenticationServices AuthenticationServices { get; }
        Task<int> CompleteAsync();
    }
}
