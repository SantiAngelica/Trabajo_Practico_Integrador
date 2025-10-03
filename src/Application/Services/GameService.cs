using Application.Helpers;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly IPropertyRepository _propertyRepository;

    public GameService(IGameRepository gameRepository, IPropertyRepository propertyRepository)
    {
        _gameRepository = gameRepository;
        _propertyRepository = propertyRepository;
    }

    public async Task<IReadOnlyList<Game>> GetAvaialbeGames()
    {
        return await _gameRepository.Get();
    }

    public async Task<GameDto?> GetGameById(string id)
    {
        var game = await _gameRepository.GetById(id);
        if (game == null)
        {
            throw new Exception("Game not found");
        }
        return GameDto.Create(game);
    }

    public async Task<GameDto?> AddGame(RequestGameDto requestGameDto)
    {
        var schedule = await _propertyRepository.GetScheduleById(requestGameDto.Schedule_id);
        if (schedule == null)
        {
            throw new Exception("Schedule not found");
        }
        var field = await _propertyRepository.GetFieldById(requestGameDto.Field_id);
        if (field == null)
        {
            throw new Exception("Field not found");
        }
        var existingReservation = await _propertyRepository.GetExistingReservation(
            requestGameDto.Date,
            requestGameDto.Field_id,
            requestGameDto.Schedule_id
        );
        if (existingReservation != null)
        {
            throw new Exception("Field is already booked for the selected date and time");
        }
        bool IsInAWeek = GameHelper.IsInAWeek(requestGameDto.Date);
        if (!IsInAWeek)
        {
            throw new Exception("You can only book a game within a week from today");
        }
        Game newGame = new Game(
            requestGameDto.Creator_id,
            requestGameDto.Missing_players,
            requestGameDto.Date,
            schedule.StartTime,
            field.FieldType
        );

        Game createdGame = await _gameRepository.Add(
            newGame,
            new Reservation(
                requestGameDto.Schedule_id,
                newGame.Id,
                requestGameDto.Field_id,
                requestGameDto.Date
            )
        );

        return createdGame != null ? GameDto.Create(createdGame) : null;
    }

    public async Task<bool> DeleteGame(string id)
    {
        return await _gameRepository.Delete(id);
    }
}
