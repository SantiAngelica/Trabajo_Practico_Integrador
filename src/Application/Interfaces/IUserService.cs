using Application.Models;
using Infrastructure;

namespace Application.Interfaces;

public interface IUserService
{
    Task<IReadOnlyList<UserDto>> GetUsers();
    Task<UserDto> GetUserById(int Id);

    Task<bool> DeleteUser(int Id);

    Task<bool> UpdateUserRol(int id, string newRol);
}
