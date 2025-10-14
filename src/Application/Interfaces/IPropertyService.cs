using Application.Models;
using Domain.Entities;
using Domain.Enum;

namespace Application.Interfaces;

public interface IPropertyService
{
    Task<PropertyDto?> CreateProperty(RequestPropertyDto property);
    Task<IReadOnlyList<PropertyDto>> GetProperties();
    Task<PropertyDto?> GetPropertyById(string id);
    Task<PropertyDto?> UpdateProperty(string id, RequestPropertyDto updateProperty);
    Task<bool> DeleteProperty(string id);
    Task<GameDto?> HandleReservation(string reservationId, States newState);

    Task<IReadOnlyList<FieldSchedulesDto>> GetReservationsByPropertyId(
        string propertyId,
        DateOnly date
    );
}
