using Domain.Entities;
using Domain.Enum;

namespace Application.Models;

public record GameDto(
    string Id,
    UserDto Creator,
    int Schedule,
    int Field,
    DateOnly Date,
    int Missing_players,
    States State,
    PropertyDto Property
)
{
    public static GameDto Create(Game game)
    {
        return new GameDto(
            game.Id,
            UserDto.Create(game.Creator),
            game.Schedule,
            game.FieldType,
            game.Date,
            game.MissingPlayers,
            game.reservation.State,
            PropertyDto.Create(game.reservation.Schedule.Property)
        );
    }

    public static IReadOnlyList<GameDto> CreateList(IReadOnlyList<Game> games)
    {
        return games.Select(Create).ToList();
    }
}

public record RequestGameDto(
    string Creator_id,
    string Property_id,
    DateOnly Date,
    string Schedule_id,
    string Field_id,
    int Missing_players
);
