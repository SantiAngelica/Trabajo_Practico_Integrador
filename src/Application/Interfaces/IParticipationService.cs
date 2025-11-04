using Application.Models;
using Domain.Entities;
using Domain.Enum;

namespace Application.Interfaces;

public interface IParticipationService
{
    Task<ParticipationDto?> AddParticipation(ParticipationRequestDto participationRequestDto);
    Task<ParticipationsSeparateDto?> GetParticipationsByUserId(int userId);
    Task<ParticipationDto?> HandleParticipationState(int Id, int recieverId, States newState);

    Task<ParticipationDto?> GetParticipationById(int Id);

    Task<IReadOnlyList<ParticipationDto>> GetAceptedParticipationsByUserId(int userId);
}
