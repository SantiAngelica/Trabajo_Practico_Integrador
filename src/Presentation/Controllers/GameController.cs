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
    public async Task<IActionResult> GetGameById(string id)
    {
        var game = await _gameService.GetGameById(id);
        return Ok(game);
    }

    [HttpPost]
    [Authorize(Roles = "0")]
    public async Task<IActionResult> CreateGame([FromBody] RequestGameDto requestGameDto)
    {
        var createdGame = await _gameService.AddGame(requestGameDto);
        if (createdGame == null)
        {
            throw new Exception("Error when creating game");
        }
        return CreatedAtAction(nameof(GetGameById), new { id = createdGame.Id }, createdGame);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "0")]
    public async Task<IActionResult> DeleteGame(string id)
    {
        string uid = ValidatorExtension.ValidateRoleAndId(User, "", false, RolesEnum.Player);
        var result = await _gameService.DeleteGame(id, uid);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpGet("by-property/{propertyId}")]
    [Authorize(Roles = "1")]
    public async Task<IActionResult> GetGamesByPropertyId(string propertyId)
    {
        var games = await _gameService.GetGamesByPropertyId(propertyId);
        return Ok(games);
    }

    [HttpGet("by-userCreator/{uid}")]
    [Authorize(Roles = "0")]
    public async Task<IActionResult> GetGamesByUserCreator(string uid)
    {
        var games = await _gameService.GetGamesByUserCreator(uid);
        return Ok(games);
    }
}
