namespace Application.Models;

public record FieldSchedulesDto(
    string FieldId,
    int FieldType,
    List<SchedulesAvailablesDto> Schedules
) { };

public record SchedulesAvailablesDto(string ScheduleId, int Schedule, bool Available)
{
    public static SchedulesAvailablesDto Create(ScheduleDto schedule, bool available) =>
        new SchedulesAvailablesDto(schedule.Id, schedule.Schedule, available);
};
