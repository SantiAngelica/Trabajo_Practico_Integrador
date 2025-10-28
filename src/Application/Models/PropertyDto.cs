using Domain.Entities;

namespace Application.Models;

public record PropertyDto(
    string Id,
    string Name,
    string Address,
    string Zone,
    OwnerDto Owner,
    IReadOnlyCollection<FieldDto> Fields,
    IReadOnlyCollection<ScheduleDto> Schedules
)
{
    public static PropertyDto Create(Property property)
    {
        return new PropertyDto(
            property.Id,
            property.Name,
            property.Adress,
            property.Zone,
            new OwnerDto(
                property.OwnerId,
                property.Owner.Name,
                property.Owner.Email,
                property.Owner.Age
            ),
            property.Fields.Select(f => new FieldDto(f.Id, f.FieldType)).ToList(),
            property.Schedules.Select(s => new ScheduleDto(s.Id, s.StartTime)).ToList()
        );
    }

    public static IReadOnlyList<PropertyDto> CreateList(IReadOnlyList<Property> properties)
    {
        return properties.Select(Create).ToList();
    }
}

public record OwnerDto(string Id, string Name, string Email, int Age);

public record RequestPropertyDto(
    string Name,
    string Address,
    string Zone,
    string OwnerId,
    List<int> FieldsType,
    List<int> Schedules
);
