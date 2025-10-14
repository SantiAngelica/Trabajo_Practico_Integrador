using Application.Models;
using Domain.Entities;
using Domain.Enum;

namespace Application.Interfaces;

public interface IUserService
{
    Task<IReadOnlyList<UserDto>> GetUsers();
    Task<UserDto?> GetUserById(string Id);

    Task<bool> DeleteUser(string Id);

    Task<bool> UpdateUserRol(string id, RolesEnum newRol);

    Task<UserDto> UpdateUser(string id, RequestUserDto userDto);
}
