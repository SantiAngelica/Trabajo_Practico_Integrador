using Domain.Entities;
using Domain.Enum;
using Microsoft.VisualBasic;

namespace Application.Models;

public record GameDto(
    int Id,
    CreatorDto Creator,
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
            new CreatorDto(
                game.Creator.Id,
                game.Creator.Name,
                game.Creator.Email,
                game.Creator.Age
            ),
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

public record CreatorDto(int Id, string Name, string Email, int Age);

public record GameWithApplicationsDto(
    int Id,
    int Schedule,
    int Field,
    DateOnly Date,
    int Missing_players,
    List<ParticipationDto> Applications,
    List<ShortUserDto> Players,
    string PropertyName,
    string PropertyAdress,
    string PropertyZone,
    States State
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
            game.Participations.Where(p =>
                    p.Type == ParticipationType.Postulacion && p.State == States.Pendiente
                )
                .Select(ParticipationDto.Create)
                .ToList(),
            game.Participations.Where(p => p.State == States.Aceptada)
                .Select(p => ShortUserDto.Create(p.User))
                .ToList(),
            game.PropertyName,
            game.PropertyAdress,
            game.PropertyZone,
            game.Reservation != null ? game.Reservation.State : States.Aceptada
        );
    }

    public static IReadOnlyList<GameWithApplicationsDto> CreateList(IReadOnlyList<Game> games)
    {
        return games.Select(Create).ToList();
    }
}
