using Application.Interfaces;
using Application.Models;
using Domain.Interfaces;
using Infrastructure;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IReadOnlyList<UserDto>> GetUsers()
    {
        var users = await _userRepository.Get();
        return UserDto.CreateList(users);
    }

    public async Task<UserDto> GetUserById(int Id)
    {
        var user = await _userRepository.GetById(Id);
        return UserDto.Create(user);
    }

    public async Task<bool> DeleteUser(int Id)
    {
        return await _userRepository.Delete(Id);
    }

    public async Task<bool> UpdateUserRol(int id, string newRol)
    {
        return await _userRepository.UpdateUserRol(id, newRol);
    }
}
