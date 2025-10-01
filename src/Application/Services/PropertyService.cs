using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;

    public PropertyService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<PropertyDto> CreateProperty(RequestPropertyDto propertyDto)
    {
        var newProperty = new Property(
            propertyDto.Name,
            propertyDto.Address,
            propertyDto.Zone,
            propertyDto.OwnerId,
            propertyDto.FieldsType,
            propertyDto.Schedules
        );
        var createdProperty = await _propertyRepository.Create(newProperty);
        return PropertyDto.Create(createdProperty);
    }

    public async Task<IReadOnlyList<PropertyDto>> GetProperties()
    {
        var properties = await _propertyRepository.Get();
        return PropertyDto.CreateList(properties);
    }

    public async Task<PropertyDto?> GetPropertyById(string id)
    {
        var property = await _propertyRepository.GetByOwnerId(id);

        return property == null ? null : PropertyDto.Create(property);
    }

    public async Task<PropertyDto?> UpdateProperty(string id, RequestPropertyDto updateProperty)
    {
        var propertyToUpdate = new Property(
            updateProperty.Name,
            updateProperty.Address,
            updateProperty.Zone,
            "",
            updateProperty.FieldsType,
            updateProperty.Schedules
        );
        var updatedProperty = await _propertyRepository.UpdateProperty(id, propertyToUpdate);
        return updatedProperty == null ? null : PropertyDto.Create(updatedProperty);
    }

    public async Task<bool> DeleteProperty(string id)
    {
        return await _propertyRepository.Delete(id);
    }
}
