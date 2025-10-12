using Microsoft.EntityFrameworkCore;
using Smart_Parking_System.Application.Interfaces;
using Smart_Parking_System.DomainLayer.Entities;
using Smart_Parking_System.Infrastructure.Data;

namespace Smart_Parking_System.DomainLayer.Repositories
{
    public class ParkingAreaRepository : GenericRepository<ParkingArea>, IParkingAreaRepository
    {

        public ParkingAreaRepository(AppDbcontext context) : base(context) { }

        public async Task<ParkingArea?> GetByIdWithSpotsAsync(Guid id)
        {
            return await _context.ParkingAreas
                                 .Include(a => a.Spots)
                                 .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<ParkingArea?> GetByNameAsync(string name)
        {
            return await _context.ParkingAreas
                                 .FirstOrDefaultAsync(a => a.Name == name);
        }
    }
}
