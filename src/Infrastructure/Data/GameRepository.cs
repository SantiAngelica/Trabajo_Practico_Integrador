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
            .Games.Include(g => g.Reservation)
            .Where(g =>
                g.MissingPlayers > 0
                && g.Date >= DateOnly.FromDateTime(DateTime.Now)
                && (g.Reservation == null || g.Reservation.State == States.Aceptada)
            )
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Game>> GetByPropertyId(int propertyId, States reservationState)
    {
        return await _context
            .Games.Include(g => g.Reservation)
            .Where(g =>
                g.Reservation.PropertyId == propertyId
                && g.Date >= DateOnly.FromDateTime(DateTime.Now)
                && (g.Reservation.State == reservationState)
            )
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Game>> GetByUserCreatorId(int userId)
    {
        return await _context
            .Games.Include(g => g.Participations)
            .Where(g => g.CreatorId == userId)
            .ToListAsync();
    }
}
