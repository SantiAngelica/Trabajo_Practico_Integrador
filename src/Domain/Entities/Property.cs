using System;
using System.Collections.Generic;

namespace Infrastructure;

public partial class Property
{
    public int Id { get; set; }

    public int IdUserOwner { get; set; }

    public string Name { get; set; } = null!;

    public string Adress { get; set; } = null!;

    public string Zone { get; set; } = null!;

    public virtual User IdUserOwnerNavigation { get; set; } = null!;

    public virtual ICollection<PropertyTypeField> PropertyTypeFields { get; set; } = new List<PropertyTypeField>();

    public virtual ICollection<ScheduleProperty> ScheduleProperties { get; set; } = new List<ScheduleProperty>();
}
