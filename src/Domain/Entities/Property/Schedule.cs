namespace Domain.Entities;

public class Schedule
{
    public int Id { get; set; }
    public int StartTime { get; set; }
    public int PropertyId { get; set; }
    public Property Property { get; set; } = null!;

    public Schedule(int startTime)
    {
        StartTime = startTime;
    }
}
