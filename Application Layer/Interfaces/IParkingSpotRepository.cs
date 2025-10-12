using Smart_Parking_System.Application.Repositories;
using Smart_Parking_System.DomainLayer.Entities;

namespace Smart_Parking_System.Application.Interfaces
{
    public interface IParkingSpotRepository :IGenericRepository<ParkingSpot> 
    {
        Task<IEnumerable<ParkingSpot>> GetSpotsByAreaIdAsync(Guid areaId);

    }
}
