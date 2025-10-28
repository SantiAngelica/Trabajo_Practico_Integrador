using Application.Models;
using Domain.Entities;

namespace Application.Interfaces;

public interface IGameService
{
    Task<IReadOnlyList<GameDto>> GetAvaialbeGames();

    Task<GameDto?> GetGameById(string id);

    Task<GameDto?> AddGame(RequestGameDto requestGameDto);

    Task<bool> DeleteGame(string id, string uid);

    Task<IReadOnlyList<GameDto>> GetGamesByPropertyId(string propertyId);

    Task<IReadOnlyList<GameWithApplicationsDto>> GetGamesByUserCreator(string userId);
}
