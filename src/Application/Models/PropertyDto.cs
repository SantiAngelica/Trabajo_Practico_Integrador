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
        Console.WriteLine("=== Creando PropertyDto ===");
        Console.WriteLine($"Id: {property.Id}");
        Console.WriteLine($"Name: {property.Name}");
        Console.WriteLine($"Address: {property.Adress}");
        Console.WriteLine($"Zone: {property.Zone}");

        if (property.Owner != null)
        {
            Console.WriteLine("=== Propietario ===");
            Console.WriteLine($"OwnerId: {property.OwnerId}");
            Console.WriteLine($"Owner.Name: {property.Owner.Name}");
            Console.WriteLine($"Owner.Email: {property.Owner.Email}");
            Console.WriteLine($"Owner.Age: {property.Owner.Age}");
        }
        else
        {
            Console.WriteLine("⚠️ Owner es null");
        }

        if (property.Fields != null)
        {
            Console.WriteLine($"=== Fields ({property.Fields.Count}) ===");
            foreach (var field in property.Fields)
            {
                Console.WriteLine($"Field -> Id: {field.Id}, Type: {field.FieldType}");
            }
        }
        else
        {
            Console.WriteLine("⚠️ Fields es null");
        }

        if (property.Schedules != null)
        {
            Console.WriteLine($"=== Schedules ({property.Schedules.Count}) ===");
            foreach (var schedule in property.Schedules)
            {
                Console.WriteLine(
                    $"Schedule -> Id: {schedule.Id}, StartTime: {schedule.StartTime}"
                );
            }
        }
        else
        {
            Console.WriteLine("⚠️ Schedules es null");
        }

        Console.WriteLine("===============================");
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
