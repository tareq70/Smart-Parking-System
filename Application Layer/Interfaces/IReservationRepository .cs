using Smart_Parking_System.Application.Dtos.Reservation;
using Smart_Parking_System.Application.Repositories;
using Smart_Parking_System.DomainLayer.Entities;

namespace Smart_Parking_System.Application.Interfaces
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        Task<bool> HasConflictAsync(Guid spotId, DateTime startUtc, DateTime endUtc);
        Task<IEnumerable<Reservation>> GetActiveReservationsByAreaAsync(Guid areaId);
        Task<IEnumerable<ReservationDto>> GetActiveReservationByUserIdAsync(Guid userId);
        Task<Reservation?> GetActiveReservationBySpotIdAsync(Guid spotId);
        Task<IEnumerable<ReservationDto>> GetByUserIdAsync(Guid userId);
        Task<Reservation> CreateReservationAsync(Guid userId, CreateReservationDto dto);
        Task AutoCompleteReservationsAsync();








    }
}
