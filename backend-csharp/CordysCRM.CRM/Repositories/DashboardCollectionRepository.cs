using CordysCRM.CRM.Domain;
using Microsoft.EntityFrameworkCore;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// 仪表板收藏仓储实现 (Dashboard Collection Repository Implementation)
/// </summary>
public class DashboardCollectionRepository : IDashboardCollectionRepository
{
    private readonly DbContext _context;

    public DashboardCollectionRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsCollectedAsync(string dashboardId, string userId)
    {
        return await _context.Set<DashboardCollection>()
            .AnyAsync(c => c.DashboardId == dashboardId && c.UserId == userId);
    }

    public async Task<DashboardCollection> AddAsync(DashboardCollection collection)
    {
        await _context.Set<DashboardCollection>().AddAsync(collection);
        await _context.SaveChangesAsync();
        return collection;
    }

    public async Task DeleteAsync(string dashboardId, string userId)
    {
        var collection = await _context.Set<DashboardCollection>()
            .FirstOrDefaultAsync(c => c.DashboardId == dashboardId && c.UserId == userId);
        
        if (collection != null)
        {
            _context.Set<DashboardCollection>().Remove(collection);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<DashboardCollection>> GetByUserIdAsync(string userId)
    {
        return await _context.Set<DashboardCollection>()
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }
}
