using Domain.Enum;

namespace Domain.Entities;

public partial class User
{
    public User(
        string name,
        string email,
        string password,
        int age,
        string zone,
        List<int> FieldsType,
        List<string> Positions
    )
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Email = email;
        Password = password;
        Age = age;
        Zone = zone;
        Role = RolesEnum.Player;
        this.AddFields(FieldsType);
        this.AddPositions(Positions);
    }

    public void Update(
        string name,
        string email,
        int age,
        string zone,
        List<int> FieldsType,
        List<string> Positions
    )
    {
        Console.WriteLine(string.Join(", ", FieldsType));
        Console.WriteLine(string.Join(", ", Positions));
        Name = name;
        Email = email;
        Age = age;
        Zone = zone;

        this.AddFields(FieldsType);
        this.AddPositions(Positions);
    }

    public void AddPositions(List<string> positions)
    {
        foreach (var position in positions)
        {
            _userPositions.Add(new UserPosition(position));
        }
    }

    public void AddFields(List<int> fields)
    {
        foreach (var field in fields)
        {
            _userFields.Add(new UserField(field));
        }
    }

    public void AddComment(string comment)
    {
        var userComment = new UserComent { Comment = comment, User = this };
        _userComents.Add(userComment);
    }
}
