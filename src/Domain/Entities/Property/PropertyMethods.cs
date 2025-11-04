using Core.Exceptions;
using Domain.Enum;

namespace Domain.Entities;

public partial class Property
{
    public Property(
        string name,
        string adress,
        string zone,
        int ownerId,
        List<int> fields,
        List<int> schedules
    )
    {
        Name = name;
        Adress = adress;
        Zone = zone;
        OwnerId = ownerId;
        this.AddFields(fields);
        this.AddSchedules(schedules);
    }

    public void Update(
        string name,
        string adress,
        string zone,
        List<int> fields,
        List<int> schedules
    )
    {
        Name = name;
        Adress = adress;
        Zone = zone;
        this._propertyFields.Clear();
        this._propertySchedules.Clear();
        AddFields(fields);
        AddSchedules(schedules);
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

    public int GetSchedule(int scheduleId)
    {
        var sch = _propertySchedules.FirstOrDefault(sch => sch.Id == scheduleId);
        if (sch == null)
            throw new AppNotFoundException("Schedule not found");
        return sch.StartTime;
    }

    public int GetField(int fieldId)
    {
        var field = _propertyFields.FirstOrDefault(field => field.Id == fieldId);
        if (field == null)
            throw new AppNotFoundException("Schedule not found");
        return field.FieldType;
    }

    public Reservation AddReservation(
        int scheduleId,
        int fieldId,
        DateOnly date,
        States state,
        int? gameId
    )
    {
        if (!_propertyFields.Any(f => f.Id == fieldId))
            throw new AppNotFoundException("Field not found");
        if (!_propertySchedules.Any(s => s.Id == scheduleId))
            throw new AppNotFoundException("Schedule not found");
        var existingReservation = Reservations.Any(r =>
            r.Date == date && r.ScheduleId == scheduleId && r.FieldId == fieldId
        );
        if (existingReservation)
            throw new AppValidationException(
                "Field is already booked for the selected date and time"
            );

        Reservation newReservation = new Reservation(scheduleId, gameId, fieldId, date, state);
        _propertyReservations.Add(newReservation);
        return newReservation;
    }
}
