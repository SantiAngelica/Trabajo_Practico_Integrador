namespace Domain.Entities;

public class Schedule
{
    public string Id { get; set; } = null!;
    public int StartTime { get; set; }
    public string PropertyId { get; set; } = null!;
    public Property Property { get; set; } = null!;
}
