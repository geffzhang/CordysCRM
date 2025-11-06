using CordysCRM.CRM.Domain;
using CordysCRM.CRM.DTOs.System;
using CordysCRM.CRM.Repositories;
using Microsoft.Extensions.Logging;

namespace CordysCRM.CRM.Services;

/// <summary>
/// Module field service implementation
/// </summary>
public class ModuleFieldService : IModuleFieldService
{
    private readonly IModuleFieldRepository _repository;
    private readonly ILogger<ModuleFieldService> _logger;

    public ModuleFieldService(
        IModuleFieldRepository repository,
        ILogger<ModuleFieldService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ModuleFieldResponse?> GetByIdAsync(string id)
    {
        var field = await _repository.GetByIdAsync(id);
        return field == null ? null : MapToResponse(field);
    }

    public async Task<List<ModuleFieldResponse>> GetByFormIdAsync(string formId)
    {
        var fields = await _repository.GetByFormIdAsync(formId);
        return fields.Select(MapToResponse).ToList();
    }

    public async Task<ModuleFieldResponse> CreateAsync(ModuleFieldAddRequest request)
    {
        // Check if internal key already exists
        var existing = await _repository.GetByInternalKeyAsync(request.InternalKey);
        if (existing != null)
        {
            throw new InvalidOperationException($"Field with internal key '{request.InternalKey}' already exists");
        }

        var field = new ModuleField
        {
            FormId = request.FormId,
            Name = request.Name,
            InternalKey = request.InternalKey,
            Type = request.Type,
            Mobile = request.Mobile,
            Pos = request.Pos
        };

        await _repository.AddAsync(field);
        _logger.LogInformation("Created module field {Name} for form {FormId}", 
            request.Name, request.FormId);

        return MapToResponse(field);
    }

    public async Task<ModuleFieldResponse> UpdateAsync(string id, ModuleFieldUpdateRequest request)
    {
        var field = await _repository.GetByIdAsync(id);
        if (field == null)
        {
            throw new KeyNotFoundException($"Module field with id {id} not found");
        }

        field.Name = request.Name;
        field.Type = request.Type;
        field.Mobile = request.Mobile;
        field.Pos = request.Pos;

        await _repository.UpdateAsync(field);
        _logger.LogInformation("Updated module field {Id}", id);

        return MapToResponse(field);
    }

    public async Task DeleteAsync(string id)
    {
        var field = await _repository.GetByIdAsync(id);
        if (field == null)
        {
            throw new KeyNotFoundException($"Module field with id {id} not found");
        }
        
        await _repository.DeleteAsync(field);
        _logger.LogInformation("Deleted module field {Id}", id);
    }

    public async Task UpdateSortAsync(List<ModuleFieldSortRequest> requests)
    {
        foreach (var request in requests)
        {
            var field = await _repository.GetByIdAsync(request.Id);
            if (field != null)
            {
                field.Pos = request.Pos;
                await _repository.UpdateAsync(field);
            }
        }
        _logger.LogInformation("Updated sort order for {Count} module fields", requests.Count);
    }

    private static ModuleFieldResponse MapToResponse(ModuleField field)
    {
        var createTime = field.CreateTime.HasValue 
            ? DateTimeOffset.FromUnixTimeMilliseconds(field.CreateTime.Value).UtcDateTime
            : DateTime.UtcNow;
        var updateTime = field.UpdateTime.HasValue 
            ? DateTimeOffset.FromUnixTimeMilliseconds(field.UpdateTime.Value).UtcDateTime
            : DateTime.UtcNow;
            
        return new ModuleFieldResponse(
            field.Id,
            field.FormId,
            field.Name,
            field.InternalKey,
            field.Type,
            field.Mobile,
            field.Pos,
            createTime,
            updateTime
        );
    }
}
