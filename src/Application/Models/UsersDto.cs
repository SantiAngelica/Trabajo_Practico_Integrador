using Infrastructure;

namespace Application.Models;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int Age { get; set; }
    public string Rol { get; set; } = null!;
    public string Zone { get; set; } = null!;
    public ICollection<UserComentDto> UserComents { get; set; } = new List<UserComentDto>();
    public ICollection<UserFieldDto> UserFields { get; set; } = new List<UserFieldDto>();
    public ICollection<UserPositionDto> UserPositions { get; set; } = new List<UserPositionDto>();

    public static UserDto Create(User user)
    {
        var dto = new UserDto();
        dto.Id = user.Id;
        dto.Name = user.Name;
        dto.Email = user.Email;
        dto.Password = user.Password;
        dto.Age = user.Age;
        dto.Rol = user.Rol;
        dto.Zone = user.Zone;
        dto.UserComents = user
            .UserComents.Select(c => new UserComentDto { Id = c.Id, Body = c.Body })
            .ToList();
        dto.UserFields = user
            .UserFields.Select(f => new UserFieldDto { Id = f.Id, Field = f.Field })
            .ToList();
        dto.UserPositions = user
            .UserPositions.Select(p => new UserPositionDto { Id = p.Id, Position = p.Position })
            .ToList();
        return dto;
    }

    public static IReadOnlyList<UserDto> CreateList(IReadOnlyList<User> users)
    {
        var dtos = new List<UserDto>();
        foreach (var user in users)
        {
            dtos.Add(Create(user));
        }
        return dtos;
    }
}

public class UserFieldDto
{
    public int Id { get; set; }
    public string Field { get; set; }
}

public class UserComentDto
{
    public int Id { get; set; }
    public string Body { get; set; }
}

public class UserPositionDto
{
    public int Id { get; set; }
    public string Position { get; set; }
}
