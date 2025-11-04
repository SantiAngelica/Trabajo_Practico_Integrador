using System.Text.RegularExpressions;
using Application.Helpers;
using Application.Interfaces;
using Application.Models;
using Core.Exceptions;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthSecurity _authSecurity;

    public AuthService(IUserRepository userRepository, IAuthSecurity authSecurity)
    {
        _userRepository = userRepository;
        _authSecurity = authSecurity;
    }

    public async Task<string> Register(RequestUserDto user)
    {
        var existingUser = await _userRepository.GetByEmail(user.Email);
        if (existingUser != null)
        {
            throw new AppValidationException("Email is already registered.");
        }
        string? isValidUser = UserHelper.IsValidUserData(
            user.Email,
            user.Age,
            user.Positions,
            user.FieldsType
        );
        if (isValidUser != null)
        {
            throw new AppValidationException(isValidUser);
        }
        User newUser = new User(
            user.Name,
            user.Email,
            user.Password,
            user.Age,
            user.Zone,
            user.FieldsType,
            user.Positions
        );
        var createdUser = await _userRepository.Create(newUser);
        await _userRepository.SaveChangesAsync();
        if (createdUser == null)
            throw new Exception("Error when creating user");
        return _authSecurity.GeneraToken(createdUser);
    }

    public async Task<string> Login(LoginRequestUserDto loginRequestUserDto)
    {
        var user = await _userRepository.GetByEmail(loginRequestUserDto.Email);
        if (user == null)
        {
            throw new AppNotFoundException(
                $"User with email: {loginRequestUserDto.Email} not found"
            );
        }

        if (user.Password == loginRequestUserDto.Password)
        {
            return _authSecurity.GeneraToken(user);
        }
        throw new AppValidationException("Password incorrect");
    }
}
