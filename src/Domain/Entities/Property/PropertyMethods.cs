namespace Domain.Entities;

public partial class Property
{
    public Property(
        string name,
        string adress,
        string zone,
        string ownerId,
        List<int> fields,
        List<int> schedules
    )
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Adress = adress;
        Zone = zone;
        OwnerId = ownerId;
        this.AddFields(fields);
        this.AddSchedules(schedules);
    }

    public void AddFields(List<int> fields)
    {
        foreach (var field in fields)
        {
            _propertyFields.Add(new Field(field));
        }
    }

    public void AddSchedules(List<int> schedules)
    {
        foreach (var schedule in schedules)
        {
            _propertySchedules.Add(new Schedule(schedule));
        }
    }
}
