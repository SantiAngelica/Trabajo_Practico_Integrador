using Domain.Entities;
using Domain.Enum;

namespace Domain.Interfaces;

public interface IPropertyRepository : IRepositoryBase<Property>
{
    Task<Property?> GetByOwnerId(int id);
    Task<Schedule?> GetScheduleById(int scheduleId);
    Task<Field?> GetFieldById(int fieldId);

    Task<IReadOnlyList<Schedule?>> GetSchedulesByPropertyId(int propertyId);

    Task<IReadOnlyList<Field?>> GetFieldsByPropertyId(int propertyId);
}
