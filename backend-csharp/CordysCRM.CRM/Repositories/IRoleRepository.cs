using CordysCRM.CRM.Domain;
using CordysCRM.Framework.Repositories;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// Role repository interface
/// </summary>
public interface IRoleRepository : IRepository<Role>
{
    /// <summary>
    /// Get roles by organization ID
    /// </summary>
    Task<List<Role>> GetByOrganizationAsync(string organizationId);

    /// <summary>
    /// Get role by name
    /// </summary>
    Task<Role?> GetByNameAsync(string name, string organizationId);
}
