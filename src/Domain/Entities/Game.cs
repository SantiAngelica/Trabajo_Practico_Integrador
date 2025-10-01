using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Game
{
    public int Id { get; set; }
    public string CreatorId { get; set; } = null!;
    public User Creator { get; set; } = null!;
    public int MissingPlayers { get; set; }
    public DateOnly Date { get; set; }
    public int Schedule { get; set; }
    public int FieldType { get; set; }

    private List<Participation> _gameParticipations = new List<Participation>();
    public IReadOnlyCollection<Participation> Participations => _gameParticipations;

    private List<User> _users = new List<User>();
    public IReadOnlyCollection<User> Users => _users;
}
