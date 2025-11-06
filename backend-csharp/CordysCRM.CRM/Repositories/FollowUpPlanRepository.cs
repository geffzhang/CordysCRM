using CordysCRM.CRM.Domain;
using Microsoft.EntityFrameworkCore;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// 跟进计划仓储实现 (Follow-Up Plan Repository Implementation)
/// Converted from Java FollowUpPlanMapper
/// </summary>
public class FollowUpPlanRepository : IFollowUpPlanRepository
{
    private readonly DbContext _context;

    public FollowUpPlanRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<FollowUpPlan?> GetByIdAsync(string id)
    {
        return await _context.Set<FollowUpPlan>()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<FollowUpPlan> AddAsync(FollowUpPlan plan)
    {
        await _context.Set<FollowUpPlan>().AddAsync(plan);
        await _context.SaveChangesAsync();
        return plan;
    }

    public async Task<FollowUpPlan> UpdateAsync(FollowUpPlan plan)
    {
        _context.Set<FollowUpPlan>().Update(plan);
        await _context.SaveChangesAsync();
        return plan;
    }

    public async Task DeleteAsync(string id)
    {
        var plan = await GetByIdAsync(id);
        if (plan != null)
        {
            _context.Set<FollowUpPlan>().Remove(plan);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<FollowUpPlan>> QueryAsync(string organizationId, string? userId = null, int page = 1, int pageSize = 20)
    {
        var query = _context.Set<FollowUpPlan>()
            .Where(p => p.OrganizationId == organizationId);

        if (!string.IsNullOrEmpty(userId))
        {
            query = query.Where(p => p.Owner == userId);
        }

        return await query
            .OrderByDescending(p => p.CreateTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}
