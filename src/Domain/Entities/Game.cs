using System;
using System.Collections.Generic;
using Domain.Enum;

namespace Domain.Entities;

public partial class Game
{
    public string Id { get; set; } = null!;
    public string CreatorId { get; set; } = null!;
    public User Creator { get; set; } = null!;
    public int MissingPlayers { get; set; }
    public DateOnly Date { get; set; }
    public int Schedule { get; set; }
    public int FieldType { get; set; }
    public string PropertyName { get; set; }
    public string PropertyAdress { get; set; }
    public string PropertyZone { get; set; }

    public Reservation? reservation { get; set; }
    private List<Participation> _gameParticipations = new List<Participation>();
    public IReadOnlyCollection<Participation> Participations => _gameParticipations;

    public Game(
        string creatorId,
        int missingPlayers,
        DateOnly date,
        int schedule,
        int fieldType,
        string propertyName,
        string propertyAdress,
        string propertyZone
    )
    {
        Id = Guid.NewGuid().ToString();
        CreatorId = creatorId;
        MissingPlayers = missingPlayers;
        Date = date;
        Schedule = schedule;
        FieldType = fieldType;
        PropertyName = propertyName;
        PropertyAdress = propertyAdress;
        PropertyZone = propertyZone;
    }

    public Participation AddParticipation(string userId, string gameId, ParticipationType type)
    {
        Participation newParticipation = new Participation(userId, gameId, type);
        _gameParticipations.Add(newParticipation);
        return newParticipation;
    }

    public Participation? GetParticipation(string participationId)
    {
        return Participations.FirstOrDefault(p => p.Id == participationId);
    }
}
