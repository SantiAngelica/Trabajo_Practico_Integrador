using Application.Interfaces;
using Application.Models;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;

namespace Presentation.Controllers;

[ApiController]
[Route("api/games")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet]
    public async Task<IActionResult> GetGames()
    {
        var games = await _gameService.GetAvaialbeGames();
        return Ok(games);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGameById(int id)
    {
        var game = await _gameService.GetGameById(id);
        return Ok(game);
    }

    [HttpPost]
    [Authorize(Roles = "0")]
    public async Task<IActionResult> CreateGame([FromBody] RequestGameDto requestGameDto)
    {
        var userId = ValidatorExtension.ValidateRoleAndId(User, null, false, RolesEnum.Player);
        var createdGame = await _gameService.AddGame(requestGameDto, userId);

        return CreatedAtAction(nameof(GetGameById), new { id = createdGame.Id }, createdGame);
    }

    [HttpPost("external-reservation")]
    [Authorize(Roles = "0")]
    public async Task<IActionResult> CreateOnlyGame(
        [FromBody] RequestGameOnylDto requestGameOnylDto
    )
    {
        var userId = ValidatorExtension.ValidateRoleAndId(User, null, false, RolesEnum.Player);
        var createdGame = await _gameService.AddGameOnyl(requestGameOnylDto, userId);
        return CreatedAtAction(nameof(GetGameById), new { id = createdGame.Id }, createdGame);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "2")]
    public async Task<IActionResult> DeleteGame(int id)
    {
        var uid = ValidatorExtension.ValidateRoleAndId(User, null, false, RolesEnum.SuperAdmin);
        var result = await _gameService.DeleteGame(id, uid);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpGet("by-property")]
    [Authorize(Roles = "1")]
    public async Task<IActionResult> GetGamesByPropertyId([FromQuery] States reservationState = States.Pendiente)
    {
        var ownerId = ValidatorExtension.ValidateRoleAndId(User, null, true, RolesEnum.Admin);
        var games = await _gameService.GetGamesByPropertyId(ownerId, reservationState);
        return Ok(games);
    }

    [HttpGet("my-games")]
    [Authorize(Roles = "0")]
    public async Task<IActionResult> GetGamesByUserCreator()
    {
        var uid = ValidatorExtension.ValidateRoleAndId(User, null, false, RolesEnum.Player);
        var games = await _gameService.GetGamesByUserCreator(uid);
        return Ok(games);
    }
}
