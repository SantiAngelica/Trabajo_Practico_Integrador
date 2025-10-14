using Application.Interfaces;
using Application.Models;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/properties")]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertyController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    [HttpPost]
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
    public async Task<IActionResult> UpdateProperty(
        string id,
        [FromBody] RequestPropertyDto updatePropertyDto
    )
    {
        var updatedProperty = await _propertyService.UpdateProperty(id, updatePropertyDto);
        if (updatedProperty == null)
        {
            return NotFound();
        }
        return Ok(updatedProperty);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProperty(string id)
    {
        var deleted = await _propertyService.DeleteProperty(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPost("{rid}/acepted")]
    public async Task<IActionResult> HandleReservation(string rid, [FromQuery] States state)
    {
        var gameDto = await _propertyService.HandleReservation(rid, state);
        return Ok(gameDto);
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
