using CordysCRM.CRM.DTOs.System;
using CordysCRM.CRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace CordysCRM.App.Controllers;

/// <summary>
/// Department management API controller
/// </summary>
[ApiController]
[Route("api/departments")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _service;

    public DepartmentController(IDepartmentService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get department by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentResponse>> GetById(string id)
    {
        var result = await _service.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Get all departments in an organization
    /// </summary>
    [HttpGet("organization/{organizationId}")]
    public async Task<ActionResult<List<DepartmentResponse>>> GetByOrganization(string organizationId)
    {
        var result = await _service.GetByOrganizationAsync(organizationId);
        return Ok(result);
    }

    /// <summary>
    /// Get department tree structure
    /// </summary>
    [HttpGet("tree/{organizationId}")]
    public async Task<ActionResult<List<DepartmentTreeResponse>>> GetTree(string organizationId)
    {
        var result = await _service.GetTreeAsync(organizationId);
        return Ok(result);
    }

    /// <summary>
    /// Create a new department
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<DepartmentResponse>> Create([FromBody] DepartmentAddRequest request)
    {
        var result = await _service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Update a department
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<DepartmentResponse>> Update(string id, [FromBody] DepartmentUpdateRequest request)
    {
        var result = await _service.UpdateAsync(id, request);
        return Ok(result);
    }

    /// <summary>
    /// Delete a department
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
