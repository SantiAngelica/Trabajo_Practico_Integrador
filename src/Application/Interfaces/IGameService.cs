using Application.Models;
using Domain.Entities;

namespace Application.Interfaces;

public interface IGameService
{
    Task<IReadOnlyList<GameDto>> GetAvaialbeGames();

    Task<GameDto?> GetGameById(int id);

    Task<GameDto?> AddGame(RequestGameDto requestGameDto, int uid);

    Task<GameDto?> AddGameOnyl(RequestGameOnylDto requestGameOnylDto, int uid);

    Task<bool> DeleteGame(int id, int uid);

    Task<IReadOnlyList<GameDto>> GetGamesByPropertyId(int propertyId);

    Task<IReadOnlyList<GameWithApplicationsDto>> GetGamesByUserCreator(int userId);
}
