using Application.Helpers;
using Application.Interfaces;
using Application.Models;
using Core.Exceptions;
using Domain.Entities;
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

    public async Task<GameDto?> GetGameById(string id)
    {
        var game = await _gameRepository.GetById(id);
        if (game == null)
        {
            throw new AppNotFoundException("Game not found");
        }
        return GameDto.Create(game);
    }

    public async Task<GameDto?> AddGame(RequestGameDto requestGameDto)
    {
        var property = await _propertyRepository.GetById(requestGameDto.Property_id);
        if (property == null)
            throw new AppNotFoundException("Property not found");
        var schedule = await _propertyRepository.GetScheduleById(requestGameDto.Schedule_id);
        if (schedule == null)
        {
            throw new AppNotFoundException("Schedule not found");
        }
        var field = await _propertyRepository.GetFieldById(requestGameDto.Field_id);
        if (field == null)
        {
            throw new AppNotFoundException("Field not found");
        }
        var existingReservation = await _reservationRepository.GetExistingReservation(
            requestGameDto.Date,
            requestGameDto.Field_id,
            requestGameDto.Schedule_id
        );
        if (existingReservation != null)
        {
            throw new AppValidationException(
                "Field is already booked for the selected date and time"
            );
        }
        bool IsInAWeek = GameHelper.IsInAWeek(requestGameDto.Date);
        if (!IsInAWeek)
        {
            throw new AppValidationException("You can only book a game within a week from today");
        }
        Game newGame = new Game(
            requestGameDto.Creator_id,
            requestGameDto.Missing_players,
            requestGameDto.Date,
            schedule.StartTime,
            field.FieldType,
            property.Name,
            property.Adress,
            property.Zone
        );
        Reservation newReservation = new Reservation(
            requestGameDto.Schedule_id,
            newGame.Id,
            requestGameDto.Field_id,
            requestGameDto.Date
        );

        await _gameRepository.Create(newGame);
        await _reservationRepository.Create(newReservation);
        await _gameRepository.SaveChangesAsync();
        await _reservationRepository.SaveChangesAsync();

        return GameDto.Create(newGame);
    }

    public async Task<bool> DeleteGame(string id, string uid)
    {
        var game = await _gameRepository.GetById(id);
        if (game == null)
            throw new AppNotFoundException("Game not found");
        if (game.CreatorId != uid)
            throw new AppUnauthorizedException("Unauthorized");
        await _gameRepository.Delete(game);
        await _gameRepository.SaveChangesAsync();
        return true;
    }

    public async Task<IReadOnlyList<GameDto>> GetGamesByPropertyId(string propertyId)
    {
        var games = await _gameRepository.GetByPropertyId(propertyId);
        if (games == null)
        {
            throw new AppNotFoundException("No games found for the given property ID");
        }
        return GameDto.CreateList(games);
    }

    public async Task<IReadOnlyList<GameWithApplicationsDto>> GetGamesByUserCreator(string userId)
    {
        var games = await _gameRepository.GetByUserCreatorId(userId);
        if (games == null)
        {
            throw new AppNotFoundException("No games found for the given user ID");
        }
        return GameWithApplicationsDto.CreateList(games);
    }
}
