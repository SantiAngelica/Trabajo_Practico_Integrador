using System.Security.Claims;
using Application.Interfaces;
using Application.Models;
using Domain.Enum;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;

namespace Presentation.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
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
        var users = await _userService.GetUsers();
        return Ok(users);
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetUserById()
    {
        var id = ValidatorExtension.ValidateRoleAndId(User, null, false, RolesEnum.Player);
        var user = await _userService.GetUserById(id);
        return Ok(user);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "0,2")] //solo superadmin o player
    public async Task<IActionResult> DeleteUser(int id)
    {
        ValidatorExtension.ValidateRoleAndId(User, id, false, RolesEnum.SuperAdmin);
        await _userService.DeleteUser(id);
        return Ok("User deleted");
    }

    [HttpPut("rolechange/{id}/{newRole}")]
    [Authorize(Roles = "2")] //solo superadmin
    public async Task<IActionResult> UpdateUserRol(int id, RolesEnum newRole)
    {
        bool updatedUser = await _userService.UpdateUserRol(id, newRole);
        return Ok("User role updated successfully");
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "0")] //solo player
    public async Task<IActionResult> UpdateUser(int id, UpdateUserDto userDto)
    {
        ValidatorExtension.ValidateRoleAndId(User, id, true, RolesEnum.Player);
        var updatedUser = await _userService.UpdateUser(id, userDto);
        return Ok(updatedUser);
    }
}
