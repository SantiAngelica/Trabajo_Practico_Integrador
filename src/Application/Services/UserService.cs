using System.Collections.Concurrent;
using System.Runtime.ConstrainedExecution;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;

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

    public async Task<UserDto> GetUserById(string Id)
    {
        var user = await _userRepository.GetById(Id);
        return UserDto.Create(user);
    }

    public async Task<bool> DeleteUser(string Id)
    {
        return await _userRepository.Delete(Id);
    }

    public async Task<bool> UpdateUserRol(string id, RolesEnum newRol)
    {
        return await _userRepository.UpdateUserRol(id, newRol);
    }

    public async Task<UserDto> UpdateUser(string id, RequestUserDto userDto)
    {
        var updatedUser = await _userRepository.UpdateUser(
            id,
            new User(
                userDto.Name,
                userDto.Email,
                "",
                userDto.Age,
                userDto.Zone,
                userDto.FieldsType,
                userDto.Positions
            )
        );
        return UserDto.Create(updatedUser);
    }
}
