using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class PropertyRepository : EfRepository<Property>, IPropertyRepository
{
    public PropertyRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public override async Task<IReadOnlyList<Property>> GetAll()
    {
        return await _context
            .Propertys.Include(p => p.Fields)
            .Include(p => p.Schedules)
            .Include(p => p.Owner)
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<Property?> GetByOwnerId(string id)
    {
        return await _context
            .Propertys.Include(p => p.Fields)
            .Include(p => p.Schedules)
            .Include(p => p.Owner)
            .AsSplitQuery()
            .FirstOrDefaultAsync(p => p.OwnerId == id);
    }

    public override async Task<Property?> Update(string id, Property updateProperty)
    {
        var existingProperty = await _context
            .Propertys.Include(p => p.Fields)
            .Include(p => p.Schedules)
            .Include(p => p.Owner)
            .AsSplitQuery()
            .FirstOrDefaultAsync(p => p.Id == id);
        if (existingProperty == null)
        {
            return null;
        }

        _context.Schedules.RemoveRange(existingProperty.Schedules);
        _context.Fields.RemoveRange(existingProperty.Fields);

        existingProperty.Name = updateProperty.Name;
        existingProperty.Adress = updateProperty.Adress;
        existingProperty.Zone = updateProperty.Zone;
        existingProperty.AddFields(updateProperty.Fields.Select(f => f.FieldType).ToList());
        existingProperty.AddSchedules(updateProperty.Schedules.Select(s => s.StartTime).ToList());

        await _context.SaveChangesAsync();

        return existingProperty;
    }

    public override async Task<bool> Delete(string id)
    {
        var property = await _context.Propertys.FirstOrDefaultAsync(p => p.Id == id);
        if (property == null)
        {
            return false;
        }

        _context.Propertys.Remove(property);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Schedule?> GetScheduleById(string scheduleId)
    {
        return await _context.Schedules.FirstOrDefaultAsync(s => s.Id == scheduleId);
    }

    public async Task<Field?> GetFieldById(string fieldId)
    {
        return await _context.Fields.FirstOrDefaultAsync(f => f.Id == fieldId);
    }

    public async Task<IReadOnlyList<Schedule?>> GetSchedulesByPropertyId(string propertyId)
    {
        return await _context.Schedules.Where(s => s.PropertyId == propertyId).ToListAsync();
    }

    public async Task<IReadOnlyList<Field?>> GetFieldsByPropertyId(string propertyId)
    {
        return await _context.Fields.Where(f => f.PropertyId == propertyId).ToListAsync();
    }
}
