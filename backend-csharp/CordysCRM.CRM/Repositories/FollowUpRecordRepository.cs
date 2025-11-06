using CordysCRM.CRM.Domain;
using Microsoft.EntityFrameworkCore;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// 跟进记录仓储实现 (Follow-Up Record Repository Implementation)
/// Converted from Java FollowUpRecordMapper
/// </summary>
public class FollowUpRecordRepository : IFollowUpRecordRepository
{
    private readonly DbContext _context;

    public FollowUpRecordRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<FollowUpRecord?> GetByIdAsync(string id)
    {
        return await _context.Set<FollowUpRecord>()
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<FollowUpRecord> AddAsync(FollowUpRecord record)
    {
        await _context.Set<FollowUpRecord>().AddAsync(record);
        await _context.SaveChangesAsync();
        return record;
    }

    public async Task<FollowUpRecord> UpdateAsync(FollowUpRecord record)
    {
        _context.Set<FollowUpRecord>().Update(record);
        await _context.SaveChangesAsync();
        return record;
    }

    public async Task DeleteAsync(string id)
    {
        var record = await GetByIdAsync(id);
        if (record != null)
        {
            _context.Set<FollowUpRecord>().Remove(record);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<FollowUpRecord>> QueryAsync(string organizationId, string? userId = null, int page = 1, int pageSize = 20)
    {
        var query = _context.Set<FollowUpRecord>()
            .Where(r => r.OrganizationId == organizationId);

        if (!string.IsNullOrEmpty(userId))
        {
            query = query.Where(r => r.Owner == userId);
        }

        return await query
            .OrderByDescending(r => r.FollowTime.HasValue ? r.FollowTime.Value : r.CreateTime ?? 0)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}
