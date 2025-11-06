using CordysCRM.CRM.Domain;
using CordysCRM.Framework.Repositories;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// Department repository interface
/// </summary>
public interface IDepartmentRepository : IRepository<Department>
{
    /// <summary>
    /// Get departments by organization ID
    /// </summary>
    Task<List<Department>> GetByOrganizationAsync(string organizationId);

    /// <summary>
    /// Get child departments
    /// </summary>
    Task<List<Department>> GetChildrenAsync(string parentId);

    /// <summary>
    /// Get department tree structure
    /// </summary>
    Task<List<Department>> GetTreeAsync(string organizationId);
}
