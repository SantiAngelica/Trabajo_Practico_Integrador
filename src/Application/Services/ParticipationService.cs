using System.ComponentModel;
using Application.Interfaces;
using Application.Models;
using Core.Exceptions;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;

namespace Application.Services;

public class ParticipationService : IParticipationService
{
    private readonly IGameRepository _gameRepository;
    private readonly IUserRepository _userRepository;

    private readonly IParticipationRepository _participationRepository;

    public ParticipationService(
        IGameRepository gameRepository,
        IUserRepository userRepository,
        IParticipationRepository participationRepository
    )
    {
        _gameRepository = gameRepository;
        _userRepository = userRepository;
        _participationRepository = participationRepository;
    }

    public async Task<ParticipationDto?> AddParticipation(
        ParticipationRequestDto participationRequestDto
    )
    {
        var user = await _userRepository.GetById(participationRequestDto.UserId);
        if (user == null)
            throw new AppNotFoundException("User not found");
        var game = await _gameRepository.GetById(participationRequestDto.GameId);
        if (game == null)
            throw new AppNotFoundException("Game not found");

        var newParticipation = game.AddParticipation(
            participationRequestDto.UserId,
            participationRequestDto.Type
        );

        await _gameRepository.SaveChangesAsync();

        return ParticipationDto.Create(newParticipation);
    }

    public async Task<ParticipationsSeparateDto?> GetParticipationsByUserId(int userId)
    {
        var participations = await _participationRepository.GetByUserId(userId);
        if (participations == null)
            throw new AppNotFoundException("User not found");

        return ParticipationsSeparateDto.CreateList(participations);
    }

    public async Task<ParticipationDto?> HandleParticipationState(
        int id,
        int recieverId,
        States newState
    )
    {
        var participation = await _participationRepository.GetById(id);
        if (participation == null)
            throw new AppNotFoundException("Participation not found");
        if (
            (
                participation.Type == ParticipationType.Invitacion
                && participation.UserId != recieverId
            )
            || (
                participation.Type == ParticipationType.Postulacion
                && participation.Game.CreatorId != recieverId
            )
        )
            throw new AppUnauthorizedException("Unauthorized");

        participation.State = newState;
        await _participationRepository.SaveChangesAsync();

        return ParticipationDto.Create(participation);
    }

    public async Task<ParticipationDto?> GetParticipationById(int Id)
    {
        var participation = await _participationRepository.GetById(Id);
        if (participation == null)
            throw new AppNotFoundException("Participation not found");

        return ParticipationDto.Create(participation);
    }

    public async Task<IReadOnlyList<ParticipationDto>> GetAceptedParticipationsByUserId(int userId)
    {
        var participations = await _participationRepository.GetAceptedByUserId(userId);
        if (participations == null)
        {
            throw new AppNotFoundException("Participations not found for the User Id given");
        }
        return ParticipationDto.CreateList(participations);
    }
}
