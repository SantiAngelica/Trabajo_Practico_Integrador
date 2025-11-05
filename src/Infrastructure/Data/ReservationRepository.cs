using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ReservationRepository : EfRepository<Reservation>, IReservationRepository
{
    public ReservationRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<Reservation?> GetExistingReservation(
        DateOnly date,
        int fieldId,
        int scheduleId
    )
    {
        return await _context.Reservations.FirstOrDefaultAsync(r =>
            r.Date == date && r.FieldId == fieldId && r.ScheduleId == scheduleId
        );
    }

    public async Task<Reservation?> GetReservationByGameId(int gameId)
    {
        return await _context.Reservations.FirstOrDefaultAsync(r => r.GameId == gameId);
    }

    public async Task<IReadOnlyList<Reservation?>> GetReservationsByPropertyId(
        int propertyId,
        DateOnly date
    )
    {
        return await _context
            .Reservations.Where(r =>
                r.PropertyId == propertyId && r.State == States.Aceptada && r.Date == date
            )
            .AsSplitQuery()
            .ToListAsync();
    }
}
