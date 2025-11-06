using CordysCRM.CRM.Domain;
using Microsoft.EntityFrameworkCore;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// 仪表板模块仓储实现 (Dashboard Module Repository Implementation)
/// Converted from Java DashboardModuleMapper
/// </summary>
public class DashboardModuleRepository : IDashboardModuleRepository
{
    private readonly DbContext _context;

    public DashboardModuleRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<DashboardModule?> GetByIdAsync(string id)
    {
        return await _context.Set<DashboardModule>()
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<DashboardModule> AddAsync(DashboardModule module)
    {
        await _context.Set<DashboardModule>().AddAsync(module);
        await _context.SaveChangesAsync();
        return module;
    }

    public async Task<DashboardModule> UpdateAsync(DashboardModule module)
    {
        _context.Set<DashboardModule>().Update(module);
        await _context.SaveChangesAsync();
        return module;
    }

    public async Task DeleteAsync(string id)
    {
        var module = await GetByIdAsync(id);
        if (module != null)
        {
            _context.Set<DashboardModule>().Remove(module);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<DashboardModule>> GetByOrganizationIdAsync(string organizationId)
    {
        return await _context.Set<DashboardModule>()
            .Where(m => m.OrganizationId == organizationId)
            .OrderBy(m => m.Pos)
            .ToListAsync();
    }
}
