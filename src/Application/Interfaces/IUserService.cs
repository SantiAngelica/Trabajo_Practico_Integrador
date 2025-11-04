using Application.Models;
using Domain.Entities;
using Domain.Enum;

namespace Application.Interfaces;

public interface IUserService
{
    Task<IReadOnlyList<UserDto>> GetUsers();
    Task<UserDto?> GetUserById(int Id);

    Task<bool> DeleteUser(int Id);

    Task<bool> UpdateUserRol(int id, RolesEnum newRol);

    Task<UserDto> UpdateUser(int id, UpdateUserDto userDto);
}
