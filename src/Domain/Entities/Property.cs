using System;
using System.Collections.Generic;

namespace Infrastructure;

public partial class Property
{
    public int Id { get; set; }

    public int? IdUserOwner { get; set; }

    public string? Name { get; set; }

    public string? Adress { get; set; }

    public string? Zone { get; set; }

    public virtual ICollection<PropertyTypeField> PropertyTypeFields { get; set; } = new List<PropertyTypeField>();

    public virtual ICollection<ScheduleProperty> ScheduleProperties { get; set; } = new List<ScheduleProperty>();
}
