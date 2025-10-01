using Application.Models;
using Domain.Entities;

namespace Application.Interfaces;

public interface IPropertyService
{
    Task<PropertyDto> CreateProperty(RequestPropertyDto property);
    Task<IReadOnlyList<PropertyDto>> GetProperties();
    Task<PropertyDto?> GetPropertyById(string id);
    Task<PropertyDto?> UpdateProperty(string id, RequestPropertyDto updateProperty);
    Task<bool> DeleteProperty(string id);
}

