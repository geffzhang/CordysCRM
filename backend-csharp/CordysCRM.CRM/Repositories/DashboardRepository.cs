using CordysCRM.CRM.Domain;
using Microsoft.EntityFrameworkCore;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// 仪表板仓储实现 (Dashboard Repository Implementation)
/// Converted from Java DashboardMapper
/// </summary>
public class DashboardRepository : IDashboardRepository
{
    private readonly DbContext _context;

    public DashboardRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<Dashboard?> GetByIdAsync(string id)
    {
        return await _context.Set<Dashboard>()
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Dashboard> AddAsync(Dashboard dashboard)
    {
        await _context.Set<Dashboard>().AddAsync(dashboard);
        await _context.SaveChangesAsync();
        return dashboard;
    }

    public async Task<Dashboard> UpdateAsync(Dashboard dashboard)
    {
        _context.Set<Dashboard>().Update(dashboard);
        await _context.SaveChangesAsync();
        return dashboard;
    }

    public async Task DeleteAsync(string id)
    {
        var dashboard = await GetByIdAsync(id);
        if (dashboard != null)
        {
            _context.Set<Dashboard>().Remove(dashboard);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Dashboard>> QueryAsync(string organizationId, int page = 1, int pageSize = 20)
    {
        return await _context.Set<Dashboard>()
            .Where(d => d.OrganizationId == organizationId)
            .OrderBy(d => d.Pos)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}
