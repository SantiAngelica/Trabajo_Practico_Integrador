using Application.Models;
using Domain.Entities;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<string> Register(RequestUserDto user);
    Task<string> Login(LoginRequestUserDto loginRequestUserDto);
}
