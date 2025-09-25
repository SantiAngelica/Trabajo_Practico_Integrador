using System.Net;
using Infrastructure;

namespace Application.Models;

public record UserDto(
    int Id,
    string Name,
    string Email,
    int Age,
    string Rol,
    string Zone,
    IReadOnlyCollection<UserComentDto> Comments,
    IReadOnlyCollection<UserFieldDto> FieldsType,
    IReadOnlyCollection<UserPositionDto> Positions
)
{
    public static UserDto Create(User user) =>
        new(
            user.Id,
            user.Name,
            user.Email,
            user.Age,
            user.Rol,
            user.Zone,
            user.UserComents.Select(c => new UserComentDto(c.Id, c.Body)).ToList(),
            user.UserFields.Select(f => new UserFieldDto(f.Id, f.Field)).ToList(),
            user.UserPositions.Select(p => new UserPositionDto(p.Id, p.Position)).ToList()
        );

    public static IReadOnlyList<UserDto> CreateList(IReadOnlyList<User> users)
    {
        return users.Select(Create).ToList();
    }
}

public record UpdateUserDto(
    string Name,
    string Email,
    int Age,
    string Zone,
    IReadOnlyCollection<int> FieldsType,
    IReadOnlyCollection<string> Positions
);

public record UserFieldDto(int Id, int Field);

public record UserComentDto(int Id, string Body);

public record UserPositionDto(int Id, string Position);
