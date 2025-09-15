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
        return await _context.Users.Include(u => u.UserFields).Include(u => u.UserPositions).ToListAsync();
    }
}