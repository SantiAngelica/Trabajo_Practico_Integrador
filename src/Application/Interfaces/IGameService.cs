using Application.Models;
using Domain.Entities;

namespace Application.Interfaces;

public interface IGameService
{
    Task<IReadOnlyList<Game>> GetAvaialbeGames();

    Task<GameDto?> GetGameById(string id);

    Task<GameDto?> AddGame(RequestGameDto requestGameDto);

    Task<bool> DeleteGame(string id);
}
