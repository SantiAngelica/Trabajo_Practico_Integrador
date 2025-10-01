using Domain.Entities;

namespace Domain.Interfaces;

public interface IPropertyRepository
{
    Task<Property> Create(Property property);
    Task<IReadOnlyList<Property>> Get();
    Task<Property?> GetByOwnerId(string id);
    Task<Property?> UpdateProperty(string id, Property updateProperty);
    Task<bool> Delete(string id);
}
