namespace Domain.Entities;

public class UserField
{
    public string Id { get; set; } = null!;
    public int Field { get; set; }
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;

    public UserField(int field)
    {
        Id = Guid.NewGuid().ToString();
        Field = field;
    }
}
