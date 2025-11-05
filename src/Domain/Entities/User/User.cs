using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Domain.Enum;

namespace Domain.Entities;

public partial class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int Age { get; set; }
    public string Zone { get; set; } = null!;
    public RolesEnum Role { get; set; }
    private List<UserPosition> _userPositions = new List<UserPosition>();
    public IReadOnlyCollection<UserPosition> UserPositions => _userPositions;

    private List<UserField> _userFields = new List<UserField>();
    public IReadOnlyCollection<UserField> UserFields => _userFields;

    private List<UserComent> _userComents = new List<UserComent>();
    public IReadOnlyCollection<UserComent> UserComents => _userComents;

    private List<Game> _gamesCreated = new List<Game>();
    public IReadOnlyCollection<Game> GamesCreated => _gamesCreated;

    private List<Participation> _participations = new List<Participation>();
    public IReadOnlyCollection<Participation> Participations => _participations;

    public User() { }
}
