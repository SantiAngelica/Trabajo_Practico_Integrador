using Domain.Enum;

namespace Domain.Entities;

public class Participation
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    public string GameId { get; set; } = null!;
    public Game Game { get; set; } = null!;

    public States State { get; set; }
    public ParticipationType Type { get; set; }
}
