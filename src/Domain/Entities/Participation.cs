using Domain.Enum;

namespace Domain.Entities;

public class Participation
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int GameId { get; set; }
    public Game Game { get; set; } = null!;

    public States State { get; set; }
    public ParticipationType Type { get; set; }

    public Participation(int userId, ParticipationType type)
    {
        UserId = userId;
        Type = type;
        State = States.Pendiente;
    }

    public bool HandleParticipation(States newState)
    {
        State = newState;
        return true;
    }
}
