using System.Collections.Concurrent;
using System.Runtime.ConstrainedExecution;
using Application.Helpers;
using Application.Interfaces;
using Application.Models;
using Core.Exceptions;
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
        var users = await _userRepository.GetAll();
        return UserDto.CreateList(users);
    }

    public async Task<UserDto?> GetUserById(string Id)
    {
        var user = await _userRepository.GetById(Id);
        if (user == null)
        {
            throw new AppNotFoundException("user not found");
        }
        ;
        return UserDto.Create(user);
    }

    public async Task<bool> DeleteUser(string Id)
    {
        bool IsDeleted = await _userRepository.Delete(Id);
        if (!IsDeleted)
        {
            throw new AppNotFoundException("User not found");
        }
        ;
        return true;
    }

    public async Task<bool> UpdateUserRol(string id, RolesEnum newRol)
    {
        var existingUser = await _userRepository.GetById(id);
        if (existingUser == null)
        {
            throw new AppNotFoundException("User not found");
        }
        return await _userRepository.UpdateUserRol(id, newRol);
    }

    public async Task<UserDto> UpdateUser(string id, RequestUserDto userDto)
    {
        string? isValidUser = UserHelper.IsValidUserData(userDto);
        if (isValidUser != null)
        {
            throw new AppValidationException(isValidUser);
        }
        var updatedUser = await _userRepository.Update(
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
        if (updatedUser == null)
        {
            throw new AppNotFoundException("User not found");
        }
        return UserDto.Create(updatedUser);
    }
}
