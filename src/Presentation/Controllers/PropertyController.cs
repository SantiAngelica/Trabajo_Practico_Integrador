using Application.Interfaces;
using Application.Models;
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
        try
        {
            var createdProperty = await _propertyService.CreateProperty(propertyDto);
            return CreatedAtAction(
                nameof(GetPropertyByOwnerId),
                new { id = createdProperty.Id },
                createdProperty
            );
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while processing your request: {e.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProperties()
    {
        try
        {
            var properties = await _propertyService.GetProperties();
            return Ok(properties);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while processing your request: {e.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPropertyByOwnerId(string id)
    {
        try
        {
            var property = await _propertyService.GetPropertyById(id);
            if (property == null)
            {
                return NotFound();
            }
            return Ok(property);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while processing your request: {e.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProperty(string id, [FromBody] RequestPropertyDto updatePropertyDto)
    {
        try
        {
            var updatedProperty = await _propertyService.UpdateProperty(id, updatePropertyDto);
            if (updatedProperty == null)
            {
                return NotFound();
            }
            return Ok(updatedProperty);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while processing your request: {e.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProperty(string id)
    {
        try
        {
            var deleted = await _propertyService.DeleteProperty(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while processing your request: {e.Message}");
        }
    }
}
