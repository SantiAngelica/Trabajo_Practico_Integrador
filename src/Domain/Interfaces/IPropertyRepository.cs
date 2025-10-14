using Domain.Entities;
using Domain.Enum;

namespace Domain.Interfaces;

public interface IPropertyRepository : IRepositoryBase<Property>
{
    Task<Property?> GetByOwnerId(string id);
    Task<Schedule?> GetScheduleById(string scheduleId);
    Task<Field?> GetFieldById(string fieldId);

    Task<IReadOnlyList<Schedule?>> GetSchedulesByPropertyId(string propertyId);

    Task<IReadOnlyList<Field?>> GetFieldsByPropertyId(string propertyId);
}
