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
    string PropertyName,
    string PropertyAdress,
    string PropertyZone
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
            game.PropertyName,
            game.PropertyAdress,
            game.PropertyZone
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

public record GameWithApplicationsDto(
    string Id,
    int Schedule,
    int Field,
    DateOnly Date,
    int Missing_players,
    List<ParticipationDto> Applications,
    string PropertyName,
    string PropertyAdress,
    string PropertyZone
)
{
    public static GameWithApplicationsDto Create(Game game)
    {
        return new GameWithApplicationsDto(
            game.Id,
            game.Schedule,
            game.FieldType,
            game.Date,
            game.MissingPlayers,
            game.Participations.Where(p => p.Type == ParticipationType.Postulacion)
                .Select(ParticipationDto.Create)
                .ToList(),
            game.PropertyName,
            game.PropertyAdress,
            game.PropertyZone
        );
    }

    public static IReadOnlyList<GameWithApplicationsDto> CreateList(IReadOnlyList<Game> games)
    {
        return games.Select(Create).ToList();
    }
}
