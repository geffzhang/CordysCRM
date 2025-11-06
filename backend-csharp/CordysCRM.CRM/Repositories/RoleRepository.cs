using CordysCRM.CRM.Domain;
using CordysCRM.Framework.Data;
using CordysCRM.Framework.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// Role repository implementation
/// </summary>
public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<Role>> GetByOrganizationAsync(string organizationId)
    {
        return await _dbSet
            .Where(r => r.OrganizationId == organizationId)
            .OrderBy(r => r.CreateTime)
            .ToListAsync();
    }

    public async Task<Role?> GetByNameAsync(string name, string organizationId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(r => r.Name == name && r.OrganizationId == organizationId);
    }
}
