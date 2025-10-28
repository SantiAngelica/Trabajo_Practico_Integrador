using Application.Helpers;
using Application.Interfaces;
using Application.Models;
using Core.Exceptions;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;

namespace Application.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IGameRepository _gameRepository;

    public PropertyService(
        IPropertyRepository propertyRepository,
        IReservationRepository reservationRepository,
        IGameRepository gameRepository
    )
    {
        _propertyRepository = propertyRepository;
        _reservationRepository = reservationRepository;
        _gameRepository = gameRepository;
    }

    public async Task<PropertyDto?> CreateProperty(RequestPropertyDto propertyDto)
    {
        var ExistingProperty = await _propertyRepository.GetByOwnerId(propertyDto.OwnerId);
        if (ExistingProperty != null)
            throw new AppValidationException("Owner already has a property.");

        string? IsValidPropertyData = PropertyHelper.IsValidPropertyData(
            propertyDto.FieldsType,
            propertyDto.Schedules
        );
        if (IsValidPropertyData != null)
            throw new AppValidationException(IsValidPropertyData);

        var newProperty = new Property(
            propertyDto.Name,
            propertyDto.Address,
            propertyDto.Zone,
            propertyDto.OwnerId,
            propertyDto.FieldsType,
            propertyDto.Schedules
        );
        var createdProperty = await _propertyRepository.Create(newProperty);
        if (createdProperty == null)
            throw new Exception("Erro when creating property");
        await _propertyRepository.SaveChangesAsync();
        return PropertyDto.Create(createdProperty);
    }

    public async Task<IReadOnlyList<PropertyDto>> GetProperties()
    {
        var properties = await _propertyRepository.GetAll();
        return PropertyDto.CreateList(properties);
    }

    public async Task<PropertyDto?> GetPropertyById(string id)
    {
        var property = await _propertyRepository.GetByOwnerId(id);
        if (property == null)
            throw new AppNotFoundException("Property not found.");

        return PropertyDto.Create(property);
    }

    public async Task<PropertyDto?> UpdateProperty(string id, RequestPropertyDto updateProperty)
    {
        var property = await _propertyRepository.GetById(id);
        if (property == null)
            throw new AppNotFoundException("Property not found.");

        string? IsValidPropertyData = PropertyHelper.IsValidPropertyData(
            updateProperty.FieldsType,
            updateProperty.Schedules
        );
        if (IsValidPropertyData != null)
            throw new AppValidationException(IsValidPropertyData);

        property.Update(
            updateProperty.Name,
            updateProperty.Address,
            updateProperty.Zone,
            updateProperty.FieldsType,
            updateProperty.Schedules
        );
        await _propertyRepository.SaveChangesAsync();
        return PropertyDto.Create(property);
    }

    public async Task<bool> DeleteProperty(string id)
    {
        var property = await _propertyRepository.GetById(id);
        if (property == null)
            throw new AppNotFoundException("Property not found");

        await _propertyRepository.Delete(property);
        await _propertyRepository.SaveChangesAsync();
        return true;
    }

    public async Task HandleReservation(string reservationId, string ownerId, States newState)
    {
        var reservation = await _reservationRepository.GetById(reservationId);
        if (reservation == null)
            throw new AppNotFoundException("Reservation not found");
        if (reservation.Schedule.Property.OwnerId != ownerId)
            throw new AppUnauthorizedException("Unauthorized");
        if (newState != States.Aceptada && newState != States.Rechazada)
        {
            throw new AppValidationException(
                "Invalid state. Only 'Aceptada' or 'Rechazada' are allowed."
            );
        }
        reservation.State = newState;
        await _reservationRepository.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<FieldSchedulesDto>> GetReservationsByPropertyId(
        string propertyId,
        DateOnly date
    )
    {
        var property = await _propertyRepository.GetById(propertyId);
        if (property == null)
        {
            throw new AppNotFoundException("Property not found.");
        }

        var fields = await _propertyRepository.GetFieldsByPropertyId(propertyId);
        var schedules = await _propertyRepository.GetSchedulesByPropertyId(propertyId);
        var reservations = await _reservationRepository.GetReservationsByPropertyId(
            propertyId,
            date
        );

        if (fields.Count == 0 || schedules.Count == 0)
        {
            throw new AppValidationException("Property has no fields or schedules.");
        }

        var fieldSchedulesList = new List<FieldSchedulesDto>();

        foreach (var field in fields)
        {
            var scheduleAvailablesList = new List<SchedulesAvailablesDto>();

            foreach (var schedule in schedules)
            {
                bool isAvailable = !reservations.Any(r =>
                    r.FieldId == field.Id && r.ScheduleId == schedule.Id
                );

                scheduleAvailablesList.Add(
                    SchedulesAvailablesDto.Create(
                        new ScheduleDto(schedule.Id, schedule.StartTime),
                        isAvailable
                    )
                );
            }

            fieldSchedulesList.Add(
                new FieldSchedulesDto(field.Id, field.FieldType, scheduleAvailablesList)
            );
        }

        return fieldSchedulesList;
    }
}
