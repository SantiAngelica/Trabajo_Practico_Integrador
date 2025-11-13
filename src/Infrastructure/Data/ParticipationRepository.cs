using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ParticipationRepository : EfRepository<Participation>, IParticipationRepository
{
    public ParticipationRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<List<Participation>> GetByUserId(int userId)
    {
        return await _context
            .Participations.Include(p => p.Game.Reservation)
            .Where(p => p.Game.Date >= DateOnly.FromDateTime(DateTime.Now) && p.UserId == userId)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Participation>> GetAceptedByUserId(int userId)
    {
        return await _context
            .Participations.Where(p =>
                p.Game.Date >= DateOnly.FromDateTime(DateTime.Now)
                && p.UserId == userId
                && p.State == States.Aceptada
            )
            .ToListAsync();
    }

    public async Task<Participation?> GetByUserIdAndGameId(int userId, int gameId)
    {
        return await _context.Participations.FirstOrDefaultAsync(p =>
            p.UserId == userId && p.GameId == gameId
        );
    }
}
