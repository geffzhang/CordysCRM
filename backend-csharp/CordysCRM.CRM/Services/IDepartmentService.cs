using CordysCRM.CRM.Domain;
using CordysCRM.CRM.DTOs.System;

namespace CordysCRM.CRM.Services;

/// <summary>
/// Department service interface
/// </summary>
public interface IDepartmentService
{
    Task<DepartmentResponse?> GetByIdAsync(string id);
    Task<List<DepartmentResponse>> GetByOrganizationAsync(string organizationId);
    Task<List<DepartmentTreeResponse>> GetTreeAsync(string organizationId);
    Task<DepartmentResponse> CreateAsync(DepartmentAddRequest request);
    Task<DepartmentResponse> UpdateAsync(string id, DepartmentUpdateRequest request);
    Task DeleteAsync(string id);
}
