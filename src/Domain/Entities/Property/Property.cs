using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Property
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Adress { get; set; } = null!;

    public string Zone { get; set; } = null!;

    public string OwnerId { get; set; } = null!;
    public User Owner { get; set; } = null!;

    private List<Schedule> _propertySchedules = new List<Schedule>();
    public IReadOnlyCollection<Schedule> Schedules => _propertySchedules;

    public List<Field> _propertyFields = new List<Field>();
    public IReadOnlyCollection<Field> Fields => _propertyFields;
}
