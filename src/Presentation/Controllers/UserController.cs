using Application.Interfaces;
using Application.Models;
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
    public async Task<IActionResult> GetUserById(int id)
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
    public async Task<IActionResult> DeleteUser(int id)
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

    [HttpPut("rolechange/{id}")]
    public async Task<IActionResult> UpdateUserRol(int id, [FromBody] string newRole)
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
}
