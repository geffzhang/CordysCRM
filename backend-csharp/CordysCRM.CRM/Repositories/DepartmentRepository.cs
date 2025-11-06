using CordysCRM.CRM.Domain;
using CordysCRM.Framework.Data;
using CordysCRM.Framework.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// Department repository implementation
/// </summary>
public class DepartmentRepository : Repository<Department>, IDepartmentRepository
{
    public DepartmentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<Department>> GetByOrganizationAsync(string organizationId)
    {
        return await _dbSet
            .Where(d => d.OrganizationId == organizationId)
            .OrderBy(d => d.Pos)
            .ToListAsync();
    }

    public async Task<List<Department>> GetChildrenAsync(string parentId)
    {
        return await _dbSet
            .Where(d => d.ParentId == parentId)
            .OrderBy(d => d.Pos)
            .ToListAsync();
    }

    public async Task<List<Department>> GetTreeAsync(string organizationId)
    {
        return await _dbSet
            .Where(d => d.OrganizationId == organizationId)
            .OrderBy(d => d.Pos)
            .ToListAsync();
    }
}
