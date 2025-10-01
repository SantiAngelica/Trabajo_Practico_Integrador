using Application.Interfaces;
using Application.Models;
using Domain.Enum;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while processing your request: {e.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        try
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while processing your request: {e.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        try
        {
            var result = await _userService.DeleteUser(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while processing your request: {e.Message}");
        }
    }

    [HttpPut("rolechange/{id}/{newRole}")]
    public async Task<IActionResult> UpdateUserRol(string id, RolesEnum newRole)
    {
        try
        {
            bool updatedUser = await _userService.UpdateUserRol(id, newRole);
            if (updatedUser == false)
            {
                return NotFound();
            }
            return Ok("User role updated successfully");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while processing your request: {e.Message}");
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, RequestUserDto userDto)
    {
        try
        {
            var updatedUser = await _userService.UpdateUser(id, userDto);
            if (updatedUser == null)
            {
                return NotFound();
            }
            return Ok(updatedUser);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while processing your request: {e.Message}");
        }
    }
}
