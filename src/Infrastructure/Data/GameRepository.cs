using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class GameRepository : IGameRepository
{
    private readonly ApplicationDbContext _context;

    public GameRepository(ApplicationDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<IReadOnlyList<Game>> Get()
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
                && g.reservation.State == Domain.Enum.States.Aceptada
            )
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<Game?> GetById(string id)
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

    public async Task<Game?> Add(Game game, Reservation reservation)
    {
        _context.Games.Add(game);
        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();
        return await this.GetById(game.Id);
    }

    public async Task<bool> Delete(string id)
    {
        var existingGame = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
        if (existingGame == null)
        {
            return false;
        }
        _context.Games.Remove(existingGame);
        await _context.SaveChangesAsync();
        return true;
    }
}
