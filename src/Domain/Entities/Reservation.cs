using System;
using System.Collections.Generic;
using Domain.Enum;

namespace Domain.Entities;

public partial class Reservation
{
    public int Id { get; set; }

    public int ScheduleId { get; set; }

    public int? GameId { get; set; } = null!;

    public Game? Game { get; set; }

    public int FieldId { get; set; }

    public int PropertyId { get; set; }

    public DateOnly Date { get; set; }

    public States State { get; set; }

    public Reservation(int scheduleId, int? gameId, int fieldId, DateOnly date, States state)
    {
        ScheduleId = scheduleId;
        GameId = gameId == null ? null : gameId;
        FieldId = fieldId;
        Date = date;
        State = state;
    }
}
