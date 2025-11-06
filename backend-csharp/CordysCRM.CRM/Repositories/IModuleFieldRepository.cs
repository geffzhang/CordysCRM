using CordysCRM.CRM.Domain;
using CordysCRM.Framework.Repositories;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// Module field repository interface
/// </summary>
public interface IModuleFieldRepository : IRepository<ModuleField>
{
    /// <summary>
    /// Get fields by form ID
    /// </summary>
    Task<List<ModuleField>> GetByFormIdAsync(string formId);

    /// <summary>
    /// Get field by internal key
    /// </summary>
    Task<ModuleField?> GetByInternalKeyAsync(string internalKey);
}
