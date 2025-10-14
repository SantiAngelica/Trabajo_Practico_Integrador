using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class UserRepository : EfRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public override async Task<IReadOnlyList<User>> GetAll()
    {
        return await _context
            .Users.Include(u => u.UserFields)
            .Include(u => u.UserPositions)
            .Include(u => u.UserComents)
            .ToListAsync();
    }

    public override async Task<User?> GetById(string Id)
    {
        return await _context
            .Users.Include(u => u.UserFields)
            .Include(u => u.UserPositions)
            .Include(u => u.UserComents)
            .FirstOrDefaultAsync(u => u.Id == Id);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> UpdateUserRol(string id, RolesEnum newRol)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            return false;
        }

        user.Role = newRol;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public override async Task<User?> Update(string id, User UpdateUser)
    {
        var user = await _context
            .Users.Include(u => u.UserPositions)
            .Include(u => u.UserFields)
            .FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            return null;
        }
        _context.UserFields.RemoveRange(_context.UserFields.Where(uf => uf.UserId == id));
        _context.UserPositions.RemoveRange(_context.UserPositions.Where(up => up.UserId == id));
        user.Update(
            UpdateUser.Name,
            UpdateUser.Email,
            UpdateUser.Age,
            UpdateUser.Zone,
            UpdateUser.UserFields.Select(f => f.Field).ToList(),
            UpdateUser.UserPositions.Select(p => p.Position).ToList()
        );
        await _context.SaveChangesAsync();
        return user;
    }
}
