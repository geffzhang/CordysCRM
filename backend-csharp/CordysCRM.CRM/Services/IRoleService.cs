using CordysCRM.CRM.DTOs.System;

namespace CordysCRM.CRM.Services;

/// <summary>
/// Role service interface
/// </summary>
public interface IRoleService
{
    Task<RoleResponse?> GetByIdAsync(string id);
    Task<List<RoleResponse>> GetByOrganizationAsync(string organizationId);
    Task<RoleResponse> CreateAsync(RoleAddRequest request);
    Task<RoleResponse> UpdateAsync(string id, RoleUpdateRequest request);
    Task DeleteAsync(string id);
}
