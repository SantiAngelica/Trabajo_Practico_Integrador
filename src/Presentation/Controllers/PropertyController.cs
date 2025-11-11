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
        var userId = ValidatorExtension.ValidateRoleAndId(User, null, false, RolesEnum.Admin);
        var createdProperty = await _propertyService.CreateProperty(propertyDto, userId);
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

    [HttpGet("my-property")]
    public async Task<IActionResult> GetPropertyByOwnerId()
    {
        var ownerId = ValidatorExtension.ValidateRoleAndId(User, null, false, RolesEnum.Admin);
        var property = await _propertyService.GetPropertyById(ownerId);
        return Ok(property);
    }

    [HttpPut("{id}")]
    // [Authorize(Roles = "1")] //solo admin
    public async Task<IActionResult> UpdateProperty([FromBody] RequestPropertyDto updatePropertyDto, int id)
    {
        var updatedProperty = await _propertyService.UpdateProperty(id, updatePropertyDto);
        if (updatedProperty == null)
        {
            return NotFound();
        }
        return Ok(updatedProperty);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "1")]
    public async Task<IActionResult> DeleteProperty(int id)
    {
        ValidatorExtension.ValidateRoleAndId(User, id, true, RolesEnum.Admin);
        var deleted = await _propertyService.DeleteProperty(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPost("{gid}/acepted")]
    [Authorize(Roles = "1")]
    public async Task<IActionResult> HandleReservation(int gid, [FromQuery] States state)
    {
        var uid = ValidatorExtension.ValidateRoleAndId(User, null, false, RolesEnum.Admin);
        await _propertyService.HandleReservation(gid, uid, state);
        return Ok();
    }

    [HttpGet("property-schedules")]
    public async Task<IActionResult> GetReservationsByPropertyId(
        [FromQuery] int propertyId,
        [FromQuery] DateOnly date
    )
    {
        var reservations = await _propertyService.GetReservationsByPropertyId(propertyId, date);
        return Ok(reservations);
    }

    [HttpPut("crossout-schedule/{propertyId}")]
    [Authorize(Roles = "1")]
    public async Task<IActionResult> CrossOutSchedule(
        int propertyId,
        [FromBody] RequestCrossOut requestCrossOut
    )
    {
        var userId = ValidatorExtension.ValidateRoleAndId(User, null, false, RolesEnum.Admin);
        await _propertyService.CrossOutSchedule(requestCrossOut, propertyId, userId);
        return await this.GetReservationsByPropertyId(propertyId, requestCrossOut.Date);
    }
}
