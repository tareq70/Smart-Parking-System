using Microsoft.EntityFrameworkCore;
using Smart_Parking_System.Application.Dtos.Reservation;
using Smart_Parking_System.Application.Enums;
using Smart_Parking_System.Application.Interfaces;
using Smart_Parking_System.DomainLayer.Entities;
using Smart_Parking_System.Infrastructure.Data;
using System.Data;

namespace Smart_Parking_System.DomainLayer.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {


        public ReservationRepository(AppDbcontext context) : base(context)
        {
            
        }
        

        public async Task<IEnumerable<Reservation>> GetActiveReservationsByAreaAsync(Guid areaId)
        {
            return await _context.Reservations
                .Include(r => r.ParkingSpot)
                .Where(r => r.ParkingSpot.ParkingAreaId == areaId && r.Status == ReservationStatus.Active)
                .ToListAsync();
        }
        public async Task<bool> HasConflictAsync(Guid spotId, DateTime startUtc, DateTime endUtc)
        {
            return await _context.Set<Reservation>()
                .Where(r => r.ParkingSpotId == spotId && r.Status == ReservationStatus.Active)
                .AnyAsync(r => !(r.EndTimeUtc <= startUtc || r.StartTimeUtc >= endUtc));
        }

        public async Task<IEnumerable<ReservationDto>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Set<ReservationDto>()
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<Reservation> CreateReservationAsync(Guid userId, CreateReservationDto dto)
        {
            if (dto.StartTimeUtc >= dto.EndTimeUtc)
                throw new ArgumentException("Start must be before end");

            // Use a transaction with Serializable isolation
            using var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);

            // 1. check spot exists
            var spot = await _context.ParkingSpots.FindAsync(dto.ParkingSpotId);
            if (spot == null) throw new InvalidOperationException("Spot not found");


            // check if spot is occupied now
            if (spot.IsOccupied)
                throw new InvalidOperationException("Spot is currently occupied and cannot be reserved.");



            // 2. check conflict
            var conflict = await _context.Reservations
                .Where(r => r.ParkingSpotId == dto.ParkingSpotId && r.Status == ReservationStatus.Active)
                .AnyAsync(r => !(r.EndTimeUtc <= dto.StartTimeUtc || r.StartTimeUtc >= dto.EndTimeUtc));
            if (conflict) throw new InvalidOperationException("Spot already reserved in this time range");

            // 3. create reservation
            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ParkingSpotId = dto.ParkingSpotId,
                StartTimeUtc = dto.StartTimeUtc,
                EndTimeUtc = dto.EndTimeUtc,
                Status = ReservationStatus.Active,
                CreatedAtUtc = DateTime.UtcNow
            };
            await _context.Reservations.AddAsync(reservation);

            // 4. update spot flag
            spot.IsReserved = true;
            _context.ParkingSpots.Update(spot);

            // 5. save
            await _context.SaveChangesAsync();
            await tx.CommitAsync();

            return reservation;
        }


    }
}
