namespace Smart_Parking_System.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IParkingAreaRepository ParkingAreas { get; }
        Task<int> CompleteAsync();
    }
}
