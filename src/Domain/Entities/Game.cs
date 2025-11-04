using System;
using System.Collections.Generic;
using Domain.Enum;

namespace Domain.Entities;

public partial class Game
{
    public int Id { get; set; }
    public int CreatorId { get; set; }
    public User Creator { get; set; } = null!;
    public int MissingPlayers { get; set; }
    public DateOnly Date { get; set; }
    public int Schedule { get; set; }
    public int FieldType { get; set; }
    public string PropertyName { get; set; }
    public string PropertyAdress { get; set; }
    public string PropertyZone { get; set; }

    public Reservation? Reservation { get; set; }
    private List<Participation> _gameParticipations = new List<Participation>();
    public IReadOnlyCollection<Participation> Participations => _gameParticipations;

    public Game(
        int creatorId,
        int missingPlayers,
        DateOnly date,
        int schedule,
        int fieldType,
        string propertyName,
        string propertyAdress,
        string propertyZone
    )
    {
        CreatorId = creatorId;
        MissingPlayers = missingPlayers;
        Date = date;
        Schedule = schedule;
        FieldType = fieldType;
        PropertyName = propertyName;
        PropertyAdress = propertyAdress;
        PropertyZone = propertyZone;
    }

    public Participation AddParticipation(int userId, ParticipationType type)
    {
        Participation newParticipation = new Participation(userId, type);
        _gameParticipations.Add(newParticipation);
        return newParticipation;
    }
}
