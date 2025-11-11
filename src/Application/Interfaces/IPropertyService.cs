using Application.Models;
using Domain.Entities;
using Domain.Enum;

namespace Application.Interfaces;

public interface IPropertyService
{
    Task<PropertyDto?> CreateProperty(RequestPropertyDto property, int ownerId);
    Task<IReadOnlyList<PropertyDto>> GetProperties();
    Task<PropertyDto?> GetPropertyById(int id);
    Task<PropertyDto?> UpdateProperty(int id, RequestPropertyDto updateProperty);
    Task<bool> DeleteProperty(int id);
    Task HandleReservation(int gameId, int ownerId, States newState);

    Task<IReadOnlyList<FieldSchedulesDto>> GetReservationsByPropertyId(
        int propertyId,
        DateOnly date
    );

    Task CrossOutSchedule(RequestCrossOut requestCrossOut, int pid, int uid);
}
