namespace Domain.Entities;

public class UserComent
{
    public string Id { get; set; } = null!;
    public string Comment { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
}
