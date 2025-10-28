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

    public override async Task<Reservation?> GetById(string id)
    {
        return await _context
            .Reservations.Include(r => r.Schedule)
            .ThenInclude(s => s.Property)
            .FirstOrDefaultAsync(r => r.Id == id);
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
}
