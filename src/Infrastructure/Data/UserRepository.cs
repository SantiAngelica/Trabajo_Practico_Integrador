using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<IReadOnlyList<User>> Get()
    {
        return await _context
            .Users.Include(u => u.UserFields)
            .Include(u => u.UserPositions)
            .Include(u => u.UserComents)
            .ToListAsync();
    }

    public async Task<User> GetById(int Id)
    {
        return await _context
            .Users.Include(u => u.UserFields)
            .Include(u => u.UserPositions)
            .Include(u => u.UserComents)
            .FirstOrDefaultAsync(u => u.Id == Id);
    }

    public async Task<bool> Delete(int Id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == Id);
        if (user == null)
        {
            return false;
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateUserRol(int id, string newRol)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            return false;
        }

        user.Rol = newRol;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }
}
