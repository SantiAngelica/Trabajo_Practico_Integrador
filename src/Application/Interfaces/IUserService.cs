using Application.Models;
using Infrastructure;

namespace Application.Interfaces;

public interface IUserService
{
    Task<IReadOnlyList<UserDto>> GetUsers();
}