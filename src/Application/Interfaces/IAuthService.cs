using Application.Models;
using Domain.Entities;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<UserDto> Register(RequestUserDto user);
    Task<string> Login(string email, string password);
}
