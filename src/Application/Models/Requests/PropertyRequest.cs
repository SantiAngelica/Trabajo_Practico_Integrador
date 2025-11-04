namespace Application.Models;

public record RequestPropertyDto(
    string Name,
    string Address,
    string Zone,
    int OwnerId,
    List<int> FieldsType,
    List<int> Schedules
);

public record RequestCrossOut(DateOnly Date, List<ScheduleField> ScheduleFields);
