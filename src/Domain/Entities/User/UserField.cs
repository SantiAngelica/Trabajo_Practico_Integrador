namespace Domain.Entities;

public class UserField
{
    public int Id { get; set; }
    public int Field { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public UserField(int field)
    {
        Field = field;
    }
}
