using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

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
        if (game == null)
        {
            return NotFound();
        }
        return Ok(game);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] RequestGameDto requestGameDto)
    {
        var createdGame = await _gameService.AddGame(requestGameDto);
        if (createdGame == null)
        {
            return BadRequest("Could not create game");
        }
        return CreatedAtAction(nameof(GetGameById), new { id = createdGame.Id }, createdGame);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGame(string id)
    {
        var result = await _gameService.DeleteGame(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpGet("/by-property/{propertyId}")]
    public async Task<IActionResult> GetGamesByPropertyId(string propertyId)
    {
        var games = await _gameService.GetGamesByPropertyId(propertyId);
        return Ok(games);
    }
}
