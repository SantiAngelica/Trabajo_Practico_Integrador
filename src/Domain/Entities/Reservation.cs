using System;
using System.Collections.Generic;
using Domain.Enum;

namespace Domain.Entities;

public partial class Reservation
{
    public int Id { get; set; }

    public string ScheduleId { get; set; } = null!;
    public Schedule Schedule { get; set; } = null!;

    public string GameId { get; set; } = null!;
    public Game Game { get; set; } = null!;

    public string FieldId { get; set; } = null!;
    public Field Field { get; set; } = null!;

    public DateOnly Date { get; set; }

    public States State { get; set; }
}
