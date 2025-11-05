namespace Application.Models;

public record FieldDto(int Id, int Field);

public record PositionDto(int Id, string Position);

public record ScheduleDto(int Id, int Schedule);

public record ScheduleField(int Schedule_Id, int Field_Id);

