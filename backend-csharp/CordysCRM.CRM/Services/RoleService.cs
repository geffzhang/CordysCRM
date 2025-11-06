using CordysCRM.CRM.Domain;
using CordysCRM.CRM.DTOs.System;
using CordysCRM.CRM.Repositories;
using Microsoft.Extensions.Logging;

namespace CordysCRM.CRM.Services;

/// <summary>
/// Role service implementation
/// </summary>
public class RoleService : IRoleService
{
    private readonly IRoleRepository _repository;
    private readonly ILogger<RoleService> _logger;

    public RoleService(
        IRoleRepository repository,
        ILogger<RoleService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<RoleResponse?> GetByIdAsync(string id)
    {
        var role = await _repository.GetByIdAsync(id);
        return role == null ? null : MapToResponse(role);
    }

    public async Task<List<RoleResponse>> GetByOrganizationAsync(string organizationId)
    {
        var roles = await _repository.GetByOrganizationAsync(organizationId);
        return roles.Select(MapToResponse).ToList();
    }

    public async Task<RoleResponse> CreateAsync(RoleAddRequest request)
    {
        // Check if role name already exists
        var existing = await _repository.GetByNameAsync(request.Name, request.OrganizationId);
        if (existing != null)
        {
            throw new InvalidOperationException($"Role with name '{request.Name}' already exists");
        }

        var role = new Role
        {
            Name = request.Name,
            OrganizationId = request.OrganizationId,
            DataScope = request.DataScope,
            Description = request.Description,
            Internal = false
        };

        await _repository.AddAsync(role);
        _logger.LogInformation("Created role {Name} in organization {OrganizationId}", 
            request.Name, request.OrganizationId);

        return MapToResponse(role);
    }

    public async Task<RoleResponse> UpdateAsync(string id, RoleUpdateRequest request)
    {
        var role = await _repository.GetByIdAsync(id);
        if (role == null)
        {
            throw new KeyNotFoundException($"Role with id {id} not found");
        }

        if (role.Internal)
        {
            throw new InvalidOperationException("Cannot update internal role");
        }

        role.Name = request.Name;
        role.DataScope = request.DataScope;
        role.Description = request.Description;

        await _repository.UpdateAsync(role);
        _logger.LogInformation("Updated role {Id}", id);

        return MapToResponse(role);
    }

    public async Task DeleteAsync(string id)
    {
        var role = await _repository.GetByIdAsync(id);
        if (role == null)
        {
            throw new KeyNotFoundException($"Role with id {id} not found");
        }

        if (role.Internal)
        {
            throw new InvalidOperationException("Cannot delete internal role");
        }

        await _repository.DeleteAsync(role);
        _logger.LogInformation("Deleted role {Id}", id);
    }

    private static RoleResponse MapToResponse(Role role)
    {
        var createTime = role.CreateTime.HasValue 
            ? DateTimeOffset.FromUnixTimeMilliseconds(role.CreateTime.Value).UtcDateTime
            : DateTime.UtcNow;
        var updateTime = role.UpdateTime.HasValue 
            ? DateTimeOffset.FromUnixTimeMilliseconds(role.UpdateTime.Value).UtcDateTime
            : DateTime.UtcNow;
            
        return new RoleResponse(
            role.Id,
            role.Name,
            role.Internal,
            role.DataScope,
            role.Description,
            role.OrganizationId,
            createTime,
            updateTime
        );
    }
}
