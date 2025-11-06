using CordysCRM.CRM.DTOs.System;
using CordysCRM.CRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace CordysCRM.App.Controllers;

/// <summary>
/// Role management API controller
/// </summary>
[ApiController]
[Route("api/roles")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _service;

    public RoleController(IRoleService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get role by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<RoleResponse>> GetById(string id)
    {
        var result = await _service.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Get all roles in an organization
    /// </summary>
    [HttpGet("organization/{organizationId}")]
    public async Task<ActionResult<List<RoleResponse>>> GetByOrganization(string organizationId)
    {
        var result = await _service.GetByOrganizationAsync(organizationId);
        return Ok(result);
    }

    /// <summary>
    /// Create a new role
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<RoleResponse>> Create([FromBody] RoleAddRequest request)
    {
        var result = await _service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Update a role
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<RoleResponse>> Update(string id, [FromBody] RoleUpdateRequest request)
    {
        var result = await _service.UpdateAsync(id, request);
        return Ok(result);
    }

    /// <summary>
    /// Delete a role
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
