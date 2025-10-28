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

    public async Task<User?> GetWithParticipations(string id)
    {
        return await _context
            .Users.Include(u => u.Participations)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}
