using Application.Interfaces;
using Application.Models;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;

namespace Presentation.Controllers;

[ApiController]
[Route("api/properties")]
[Authorize]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertyController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    [HttpPost]
    [Authorize(Roles = "1")] //solo admin
    public async Task<IActionResult> CreateProperty([FromBody] RequestPropertyDto propertyDto)
    {
        var createdProperty = await _propertyService.CreateProperty(propertyDto);
        return CreatedAtAction(
            nameof(GetPropertyByOwnerId),
            new { id = createdProperty.Id },
            createdProperty
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProperties()
    {
        var properties = await _propertyService.GetProperties();
        return Ok(properties);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPropertyByOwnerId(string id)
    {
        var property = await _propertyService.GetPropertyById(id);
        if (property == null)
        {
            return NotFound();
        }
        return Ok(property);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "1")] //solo admin
    public async Task<IActionResult> UpdateProperty(
        string id,
        [FromBody] RequestPropertyDto updatePropertyDto
    )
    {
        ValidatorExtension.ValidateRoleAndId(
            User,
            updatePropertyDto.OwnerId,
            true,
            RolesEnum.Admin
        );
        var updatedProperty = await _propertyService.UpdateProperty(id, updatePropertyDto);
        if (updatedProperty == null)
        {
            return NotFound();
        }
        return Ok(updatedProperty);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "1")]
    public async Task<IActionResult> DeleteProperty(string id)
    {
        ValidatorExtension.ValidateRoleAndId(User, id, true, RolesEnum.Admin);
        var deleted = await _propertyService.DeleteProperty(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPost("{rid}/acepted")]
    [Authorize(Roles = "1")]
    public async Task<IActionResult> HandleReservation(string rid, [FromQuery] States state)
    {
        string? uid = User.FindFirst("id")?.Value;
        await _propertyService.HandleReservation(rid, uid, state);
        return Ok();
    }

    [HttpGet("property-schedules")]
    public async Task<IActionResult> GetReservationsByPropertyId(
        [FromQuery] string propertyId,
        [FromQuery] DateOnly date
    )
    {
        var reservations = await _propertyService.GetReservationsByPropertyId(propertyId, date);
        return Ok(reservations);
    }
}
