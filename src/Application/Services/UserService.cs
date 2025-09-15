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
    
}