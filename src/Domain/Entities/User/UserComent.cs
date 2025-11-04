namespace Domain.Entities;

public class UserComent
{
    public int Id { get; set; }
    public string Comment { get; set; } = null!;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int CommenterId { get; set; }
    public User Commenter { get; set; } = null!;
}
