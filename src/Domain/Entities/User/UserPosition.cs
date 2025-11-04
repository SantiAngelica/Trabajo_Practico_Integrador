namespace Domain.Entities;

public class UserPosition
{
    public int Id { get; set; }
    public string Position { get; set; } = null!;
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public UserPosition(string position)
    {
        Position = position;
    }
}
