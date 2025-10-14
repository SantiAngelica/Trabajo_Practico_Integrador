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
        try
        {
            var createdUser = await _authService.Register(user);
            return Ok(createdUser);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while processing your request: {e.Message}");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestUserDto loginRequestUserDto)
    {
        var token = await _authService.Login(loginRequestUserDto);
        return Ok(token);
    }
}
