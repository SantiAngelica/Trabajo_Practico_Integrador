using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class GameRepository : EfRepository<Game>, IGameRepository
{
    public GameRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public override async Task<IReadOnlyList<Game>> GetAll()
    {
        return await _context
            .Games.Include(g => g.reservation)
            .Include(g => g.reservation.Schedule)
            .Include(g => g.reservation.Field)
            .Include(g => g.reservation.Schedule.Property)
            .Include(g => g.Creator)
            .Include(g => g.reservation.Schedule.Property.Owner)
            .Where(g =>
                g.MissingPlayers > 0
                && g.Date >= DateOnly.FromDateTime(DateTime.Now)
                && g.reservation.State == States.Aceptada
            )
            .AsSplitQuery()
            .ToListAsync();
    }

    public override async Task<Game?> GetById(string id)
    {
        return await _context
            .Games.Include(g => g.Creator)
            .Include(g => g.reservation)
            .Include(g => g.reservation.Schedule)
            .Include(g => g.reservation.Field)
            .Include(g => g.reservation.Schedule.Property)
            .Include(g => g.reservation.Schedule.Property.Owner)
            .AsSplitQuery()
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<IReadOnlyList<Game>> GetByPropertyId(string propertyId)
    {
        return await _context
            .Games.Include(g => g.reservation)
            .Include(g => g.reservation.Schedule)
            .Include(g => g.reservation.Field)
            .Include(g => g.reservation.Schedule.Property)
            .Include(g => g.Creator)
            .Include(g => g.reservation.Schedule.Property.Owner)
            .Where(g => g.reservation.Schedule.PropertyId == propertyId)
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<Game?> GetByReservationId(string reservationId)
    {
        return await _context
            .Games.Include(g => g.Creator)
            .Include(g => g.reservation)
            .Include(g => g.reservation.Schedule)
            .Include(g => g.reservation.Field)
            .Include(g => g.reservation.Schedule.Property)
            .Include(g => g.reservation.Schedule.Property.Owner)
            .AsSplitQuery()
            .FirstOrDefaultAsync(g => g.reservation.Id == reservationId);
    }
}
