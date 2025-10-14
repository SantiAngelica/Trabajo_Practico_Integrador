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
        string fieldId,
        string scheduleId
    )
    {
        return await _context.Reservations.FirstOrDefaultAsync(r =>
            r.Date == date && r.FieldId == fieldId && r.ScheduleId == scheduleId
        );
    }

    public async Task<IReadOnlyList<Reservation?>> GetReservationsByPropertyId(
        string propertyId,
        DateOnly date
    )
    {
        return await _context
            .Reservations.Include(r => r.Field)
            .Include(r => r.Schedule)
            .Where(r =>
                r.Schedule.PropertyId == propertyId && r.State == States.Aceptada && r.Date == date
            )
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<bool> UpdateReservationState(string reservationId, States newState)
    {
        var reservation = await _context.Reservations.FirstOrDefaultAsync(r =>
            r.Id == reservationId
        );

        if (reservation == null)
        {
            return false;
        }

        reservation.State = newState;
        await _context.SaveChangesAsync();

        return true;
    }
}
