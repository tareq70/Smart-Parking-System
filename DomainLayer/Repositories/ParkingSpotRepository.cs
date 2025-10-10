using Smart_Parking_System.Application.Interfaces;
using Smart_Parking_System.DomainLayer.Entities;
using Smart_Parking_System.Infrastructure.Data;

namespace Smart_Parking_System.DomainLayer.Repositories
{
    public class ParkingSpotRepository : GenericRepository<ParkingSpot>, IParkingSpotRepository
    {
        public ParkingSpotRepository(AppDbcontext context) : base(context) { }

        public Task<IEnumerable<ParkingSpot>> GetSpotsByAreaIdAsync(Guid areaId)
        {
            var spots = _context.ParkingSpots.Where(s => s.ParkingAreaId == areaId);
            return Task.FromResult(spots.AsEnumerable());
        }
    }
}
