using Domain.Enum;

namespace Domain.Entities;

public class Participation
{
    public string Id { get; set; }
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    public string GameId { get; set; } = null!;
    public Game Game { get; set; } = null!;

    public States State { get; set; }
    public ParticipationType Type { get; set; }

    public Participation(string userId, string gameId, ParticipationType type)
    {
        Id = Guid.NewGuid().ToString();
        UserId = userId;
        GameId = gameId;
        Type = type;
        State = States.Pendiente;
    }

    public bool HandleParticipation(States newState)
    {
        State = newState;
        return true;
    }
}
