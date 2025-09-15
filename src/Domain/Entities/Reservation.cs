using System;
using System.Collections.Generic;

namespace Infrastructure;

public partial class Reservation
{
    public int Id { get; set; }

    public int IdSchedule { get; set; }

    public int IdGame { get; set; }

    public int IdField { get; set; }

    public DateOnly Date { get; set; }

    public string? State { get; set; }

    public virtual PropertyTypeField IdFieldNavigation { get; set; } = null!;

    public virtual Game IdGameNavigation { get; set; } = null!;

    public virtual ScheduleProperty IdScheduleNavigation { get; set; } = null!;
}
