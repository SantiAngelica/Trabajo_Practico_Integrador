using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class PropertyRepository : IPropertyRepository
{
    private readonly ApplicationDbContext _context;

    public PropertyRepository(ApplicationDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<Property?> Create(Property property)
    {
        _context.Propertys.Add(property);
        await _context.SaveChangesAsync();
        return await GetByOwnerId(property.OwnerId);
    }

    public async Task<IReadOnlyList<Property>> Get()
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

    public async Task<Property?> UpdateProperty(string id, Property updateProperty)
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

    public async Task<bool> Delete(string id)
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

    public async Task<Reservation?> GetExistingReservation(
        DateOnly date,
        string fieldId,
        string scheduleId
    )
    {
        return await _context.Reservations.FirstOrDefaultAsync(r =>
            r.Date == date && r.FieldId == fieldId && r.ScheduleId == scheduleId
        );
    }
}
