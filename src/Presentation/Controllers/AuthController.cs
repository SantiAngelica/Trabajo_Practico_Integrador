using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RequestUserDto user)
    {
        var createdUser = await _authService.Register(user);
        return Ok(createdUser);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestUserDto loginRequestUserDto)
    {
        var token = await _authService.Login(loginRequestUserDto);
        return Ok(token);
    }
}
