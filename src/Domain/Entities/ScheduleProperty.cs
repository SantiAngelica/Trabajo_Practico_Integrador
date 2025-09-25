using System;
using System.Collections.Generic;

namespace Infrastructure;

public partial class ScheduleProperty
{
    public int Id { get; set; }

    public int IdProperty { get; set; }

    public int? Schedule { get; set; }

    public virtual Property IdPropertyNavigation { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
