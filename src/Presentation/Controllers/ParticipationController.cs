using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;
using SQLitePCL;

namespace Presentation.Controllers;

[ApiController]
[Route("api/participations")]
public class ParticipationController : ControllerBase
{
    private readonly IParticipationService _participationService;

    public ParticipationController(IParticipationService participationService)
    {
        _participationService = participationService;
    }

    [HttpGet("my-participations")]
    [Authorize(Roles = "0")]
    public async Task<IActionResult> GetMyParticipations()
    {
        string uid = ValidatorExtension.ValidateRoleAndId(User, "", false, RolesEnum.Player);
        var participations = await _participationService.GetParticipationsByUserId(uid);
        return Ok(participations);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetParticipationById(string id)
    {
        var participation = await _participationService.GetParticipationById(id);
        return Ok(participation);
    }

    [HttpPost]
    [Authorize(Roles = "0")]
    public async Task<IActionResult> PostParticipation(
        [FromBody] ParticipationRequestDto participationRequestDto
    )
    {
        var participation = await _participationService.AddParticipation(participationRequestDto);
        if (participation == null)
            throw new Exception("Error when creating participation");
        return CreatedAtAction(
            nameof(GetParticipationById),
            new { id = participation.Id },
            participation
        );
    }

    [HttpPost("handle/{id}")]
    [Authorize(Roles = "0")]
    public async Task<IActionResult> PostHandleParticipation(string id, [FromQuery] States newState)
    {
        string uid = ValidatorExtension.ValidateRoleAndId(User, "", false, RolesEnum.Player);
        var participation = await _participationService.HandleParticipationState(id, uid, newState);
        if (participation == null)
            throw new Exception("Error when handling participation");
        return Ok(participation);
    }

    [HttpGet("my-acepted-participations")]
    [Authorize(Roles = "0")]
    public async Task<IActionResult> GetMyAceptedParticipations()
    {
        string uid = ValidatorExtension.ValidateRoleAndId(User, "", false, RolesEnum.Player);
        var participations = await _participationService.GetAceptedParticipationsByUserId(uid);
        if (participations == null)
            throw new Exception("Error when getting participations");

        return Ok(participations);
    }
}
