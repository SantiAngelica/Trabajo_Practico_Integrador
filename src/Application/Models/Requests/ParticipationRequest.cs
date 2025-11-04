using Domain.Enum;

namespace Application.Models;

public record ParticipationRequestDto(int GameId, int UserId, ParticipationType Type);
