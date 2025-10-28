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
        var user = await _userRepository.GetById(Id);
        if (user == null)
            throw new AppNotFoundException("User not found");

        await _userRepository.Delete(user);
        await _userRepository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateUserRol(string id, RolesEnum newRol)
    {
        var user = await _userRepository.GetById(id);
        if (user == null)
            throw new AppNotFoundException("User not found");
        user.Role = newRol;
        await _userRepository.SaveChangesAsync();
        return true;
    }

    public async Task<UserDto> UpdateUser(string id, RequestUserDto userDto)
    {
        string? isValidUser = UserHelper.IsValidUserData(userDto);
        if (isValidUser != null)
            throw new AppValidationException(isValidUser);

        var user = await _userRepository.GetById(id);
        if (user == null)
            throw new AppNotFoundException("User not found");

        user.Update(
            userDto.Name,
            userDto.Email,
            userDto.Age,
            userDto.Zone,
            userDto.FieldsType,
            userDto.Positions
        );
        await _userRepository.SaveChangesAsync();
        return UserDto.Create(user);
    }
}
