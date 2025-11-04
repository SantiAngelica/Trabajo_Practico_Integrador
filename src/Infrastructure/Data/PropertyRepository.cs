using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class PropertyRepository : EfRepository<Property>, IPropertyRepository
{
    public PropertyRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<Property?> GetByOwnerId(int id)
    {
        return await _context.Propertys.FirstOrDefaultAsync(p => p.OwnerId == id);
    }

    public async Task<Schedule?> GetScheduleById(int scheduleId)
    {
        return await _context.Schedules.FirstOrDefaultAsync(s => s.Id == scheduleId);
    }

    public async Task<Field?> GetFieldById(int fieldId)
    {
        return await _context.Fields.FirstOrDefaultAsync(f => f.Id == fieldId);
    }

    public async Task<IReadOnlyList<Schedule?>> GetSchedulesByPropertyId(int propertyId)
    {
        return await _context.Schedules.Where(s => s.PropertyId == propertyId).ToListAsync();
    }

    public async Task<IReadOnlyList<Field?>> GetFieldsByPropertyId(int propertyId)
    {
        return await _context.Fields.Where(f => f.PropertyId == propertyId).ToListAsync();
    }
}
