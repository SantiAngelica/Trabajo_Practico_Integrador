namespace Application.Models;

public record FieldSchedulesDto(
    int FieldId,
    int FieldType,
    List<SchedulesAvailablesDto> Schedules
) { };

public record SchedulesAvailablesDto(int ScheduleId, int Schedule, bool Available)
{
    public static SchedulesAvailablesDto Create(ScheduleDto schedule, bool available) =>
        new SchedulesAvailablesDto(schedule.Id, schedule.Schedule, available);
};
