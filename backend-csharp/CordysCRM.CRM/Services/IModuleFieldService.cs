using CordysCRM.CRM.DTOs.System;

namespace CordysCRM.CRM.Services;

/// <summary>
/// Module field service interface
/// </summary>
public interface IModuleFieldService
{
    Task<ModuleFieldResponse?> GetByIdAsync(string id);
    Task<List<ModuleFieldResponse>> GetByFormIdAsync(string formId);
    Task<ModuleFieldResponse> CreateAsync(ModuleFieldAddRequest request);
    Task<ModuleFieldResponse> UpdateAsync(string id, ModuleFieldUpdateRequest request);
    Task DeleteAsync(string id);
    Task UpdateSortAsync(List<ModuleFieldSortRequest> requests);
}
