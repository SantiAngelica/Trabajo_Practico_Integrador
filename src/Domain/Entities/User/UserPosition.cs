namespace Domain.Entities;

public class UserPosition
{
    public string Id { get; set; } = null!;
    public string Position { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;

    public UserPosition(string position)
    {
        Id = Guid.NewGuid().ToString();
        Position = position;
    }
}
