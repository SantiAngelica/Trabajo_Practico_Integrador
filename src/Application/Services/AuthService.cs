using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Register(RequestUserDto user)
    {
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
        Console.WriteLine(
            $"Id: {createdUser.Id}, Name: {createdUser.Name}, Email: {createdUser.Email}, Age: {createdUser.Age}, Zone: {createdUser.Zone}, Role: {createdUser.Role}"
        );
        Console.WriteLine(
            "Fields: " + string.Join(", ", createdUser.UserFields.Select(f => f.Field))
        );
        Console.WriteLine(
            "Positions: " + string.Join(", ", createdUser.UserPositions.Select(p => p.Position))
        );

        return UserDto.Create(createdUser);
    }

    public Task<string> Login(string email, string password)
    {
        // Implementation for user login
        throw new NotImplementedException();
    }
}
