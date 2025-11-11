using Application.Helpers;
using Application.Interfaces;
using Application.Models;
using Core.Exceptions;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;

namespace Application.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly IPropertyRepository _propertyRepository;

    private readonly IReservationRepository _reservationRepository;

    public GameService(
        IGameRepository gameRepository,
        IPropertyRepository propertyRepository,
        IReservationRepository reservationRepository
    )
    {
        _gameRepository = gameRepository;
        _propertyRepository = propertyRepository;
        _reservationRepository = reservationRepository;
    }

    public async Task<IReadOnlyList<GameDto>> GetAvaialbeGames()
    {
        var games = await _gameRepository.GetAll();
        return GameDto.CreateList(games);
    }

    public async Task<GameDto?> GetGameById(int id)
    {
        var game = await _gameRepository.GetById(id);
        if (game == null)
        {
            throw new AppNotFoundException("Game not found");
        }
        return GameDto.Create(game);
    }

    public async Task<GameDto?> AddGame(RequestGameDto requestGameDto, int uid)
    {
        var property = await _propertyRepository.GetById(requestGameDto.Property_id);
        if (property == null)
            throw new AppNotFoundException("Property not found");

        bool IsInAWeek = GameHelper.IsInAWeek(requestGameDto.Date);
        if (!IsInAWeek)
        {
            throw new AppValidationException("You can only book a game within a week from today");
        }
        var field = property.GetField(requestGameDto.Field_id);
        var sch = property.GetSchedule(requestGameDto.Schedule_id);
        Game newGame = new Game(
            uid,
            requestGameDto.Missing_players,
            requestGameDto.Date,
            sch,
            field,
            property.Name,
            property.Adress,
            property.Zone
        );
        await _gameRepository.Create(newGame);

        await _gameRepository.SaveChangesAsync();

        property.AddReservation(
            requestGameDto.Schedule_id,
            requestGameDto.Field_id,
            requestGameDto.Date,
            States.Pendiente,
            newGame.Id
        );

        await _reservationRepository.SaveChangesAsync();

        var gameCreated = await _gameRepository.GetById(newGame.Id);
        return GameDto.Create(gameCreated);
    }

    public async Task<GameDto?> AddGameOnyl(RequestGameOnylDto requestGameOnylDto, int uid)
    {
        GameHelper.ValidateFieldAndSchedulue(
            requestGameOnylDto.FieldType,
            requestGameOnylDto.Schedule
        );

        if (!GameHelper.IsInAWeek(requestGameOnylDto.Date))
            throw new AppValidationException("You can only book a game within a week from today");

        Game newGame = new Game(
            uid,
            requestGameOnylDto.Missing_players,
            requestGameOnylDto.Date,
            requestGameOnylDto.Schedule,
            requestGameOnylDto.FieldType,
            requestGameOnylDto.PropertyName,
            requestGameOnylDto.PropertyAdress,
            requestGameOnylDto.PropertyZone
        );

        await _gameRepository.Create(newGame);
        await _gameRepository.SaveChangesAsync();

        return await this.GetGameById(newGame.Id);
    }

    public async Task<bool> DeleteGame(int id, int uid)
    {
        var game = await _gameRepository.GetById(id);
        if (game == null)
            throw new AppNotFoundException("Game not found");

        await _gameRepository.Delete(game);
        await _gameRepository.SaveChangesAsync();
        return true;
    }

    public async Task<IReadOnlyList<GameDto>> GetGamesByPropertyId(
        int ownerId,
        States reservationState
    )
    {
        var property = await _propertyRepository.GetByOwnerId(ownerId);
        if (property == null)
            throw new AppNotFoundException("Property not found for the given owner ID");
        var games = await _gameRepository.GetByPropertyId(property.Id, reservationState);
        if (games == null)
        {
            throw new AppNotFoundException("No games found for the given property ID");
        }
        return GameDto.CreateList(games);
    }

    public async Task<IReadOnlyList<GameWithApplicationsDto>> GetGamesByUserCreator(int userId)
    {
        var games = await _gameRepository.GetByUserCreatorId(userId);
        if (games == null)
        {
            throw new AppNotFoundException("No games found for the given user ID");
        }
        return GameWithApplicationsDto.CreateList(games);
    }
}
