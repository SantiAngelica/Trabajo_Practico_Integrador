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
            .Where(g =>
                g.MissingPlayers > 0
                && g.Date >= DateOnly.FromDateTime(DateTime.Now)
                && (g.reservation == null || g.reservation.State == States.Aceptada)
            )
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Game>> GetByPropertyId(string propertyId)
    {
        return await _context
            .Games.Include(g => g.reservation)
            .ThenInclude(r => r.Schedule)
            .Where(g =>
                g.reservation.Schedule.PropertyId == propertyId
                && g.Date >= DateOnly.FromDateTime(DateTime.Now)
            )
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Game>> GetByUserCreatorId(string userId)
    {
        return await _context
            .Games.Include(g => g.Participations)
            .Where(g => g.CreatorId == userId)
            .ToListAsync();
    }
}
