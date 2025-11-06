using CordysCRM.CRM.DTOs.System;
using CordysCRM.CRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace CordysCRM.App.Controllers;

/// <summary>
/// Module field configuration API controller
/// </summary>
[ApiController]
[Route("api/module-fields")]
public class ModuleFieldController : ControllerBase
{
    private readonly IModuleFieldService _service;

    public ModuleFieldController(IModuleFieldService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get module field by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ModuleFieldResponse>> GetById(string id)
    {
        var result = await _service.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Get all fields for a form
    /// </summary>
    [HttpGet("form/{formId}")]
    public async Task<ActionResult<List<ModuleFieldResponse>>> GetByFormId(string formId)
    {
        var result = await _service.GetByFormIdAsync(formId);
        return Ok(result);
    }

    /// <summary>
    /// Create a new module field
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ModuleFieldResponse>> Create([FromBody] ModuleFieldAddRequest request)
    {
        var result = await _service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Update a module field
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ModuleFieldResponse>> Update(string id, [FromBody] ModuleFieldUpdateRequest request)
    {
        var result = await _service.UpdateAsync(id, request);
        return Ok(result);
    }

    /// <summary>
    /// Delete a module field
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Update sort order for multiple fields
    /// </summary>
    [HttpPut("sort")]
    public async Task<IActionResult> UpdateSort([FromBody] List<ModuleFieldSortRequest> requests)
    {
        await _service.UpdateSortAsync(requests);
        return NoContent();
    }
}
