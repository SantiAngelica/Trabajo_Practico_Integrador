using System.Net;
using Domain.Entities;
using Domain.Enum;

namespace Application.Models;

public record UserDto(
    int Id,
    string Name,
    string Email,
    int Age,
    RolesEnum Role,
    string Zone,
    IReadOnlyCollection<string> Comments,
    IReadOnlyCollection<FieldDto> FieldsType,
    IReadOnlyCollection<PositionDto> Positions
)
{
    public static UserDto Create(User user) =>
        new UserDto(
            user.Id,
            user.Name,
            user.Email,
            user.Age,
            user.Role,
            user.Zone,
            user.UserComents.Select(c => c.Comment).ToList(),
            user.UserFields.Select(f => new FieldDto(f.Id, f.Field)).ToList(),
            user.UserPositions.Select(p => new PositionDto(p.Id, p.Position)).ToList()
        );

    public static IReadOnlyList<UserDto> CreateList(IReadOnlyList<User> users)
    {
        return users.Select(Create).ToList();
    }
}


