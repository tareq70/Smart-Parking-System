using Smart_Parking_System.Application.Repositories;
using Smart_Parking_System.DomainLayer.Entities;

namespace Smart_Parking_System.Application.Interfaces
{
    public interface IParkingAreaRepository : IGenericRepository<ParkingArea> 
    {
        Task<ParkingArea?> GetByNameAsync(string name);
        Task<ParkingArea?> GetByIdWithSpotsAsync(Guid id);

    }
}
