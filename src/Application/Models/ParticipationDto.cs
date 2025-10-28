using Domain.Entities;
using Domain.Enum;

namespace Application.Models;

public record ParticipationRequestDto(string GameId, string UserId, ParticipationType Type);

public record ParticipationDto(
    string Id,
    UserDto User,
    GameDto Game,
    ParticipationType Type,
    States State
)
{
    public static ParticipationDto Create(Participation participation)
    {
        return new ParticipationDto(
            participation.Id,
            UserDto.Create(participation.User),
            GameDto.Create(participation.Game),
            participation.Type,
            participation.State
        );
    }

    public static IReadOnlyList<ParticipationDto> CreateList(
        IReadOnlyList<Participation> participations
    )
    {
        return participations.Select(Create).ToList();
    }
}

public record ParticipationsSeparateDto(
    List<ParticipationDto> Invitations,
    List<ParticipationDto> Applications
)
{
    public static ParticipationsSeparateDto CreateList(List<Participation> participations)
    {
        List<ParticipationDto> invitationsDto = participations
            .Where(p => p.Type == ParticipationType.Invitacion)
            .Select(ParticipationDto.Create)
            .ToList();
        List<ParticipationDto> applicationsDto = participations
            .Where(p => p.Type == ParticipationType.Postulacion)
            .Select(ParticipationDto.Create)
            .ToList();

        return new ParticipationsSeparateDto(invitationsDto, applicationsDto);
    }
}
