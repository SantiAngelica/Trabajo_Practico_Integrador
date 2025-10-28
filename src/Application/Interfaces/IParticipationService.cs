using Application.Models;
using Domain.Entities;
using Domain.Enum;

namespace Application.Interfaces;

public interface IParticipationService
{
    Task<ParticipationDto?> AddParticipation(ParticipationRequestDto participationRequestDto);
    Task<ParticipationsSeparateDto?> GetParticipationsByUserId(string userId);
    Task<ParticipationDto?> HandleParticipationState(string Id, string recieverId, States newState);

    Task<ParticipationDto?> GetParticipationById(string Id);

    Task<IReadOnlyList<ParticipationDto>> GetAceptedParticipationsByUserId(string userId);
}
