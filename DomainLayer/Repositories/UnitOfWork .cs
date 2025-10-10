using Smart_Parking_System.Application.Interfaces;
using Smart_Parking_System.DomainLayer.Entities;
using Smart_Parking_System.Infrastructure.Data;

namespace Smart_Parking_System.DomainLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {    
        private readonly AppDbcontext _context;

        public IParkingAreaRepository ParkingAreas { get; } = default!;
        public IParkingSpotRepository ParkingSpots { get; }

        public UnitOfWork(AppDbcontext dbcontext, IParkingAreaRepository parkingAreas, IParkingSpotRepository parkingSpots)
        {

            _context = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
            ParkingAreas = parkingAreas ?? throw new ArgumentNullException(nameof(parkingAreas));
            ParkingSpots = parkingSpots ?? throw new ArgumentNullException(nameof(parkingSpots));
        }


        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();

    }
}
