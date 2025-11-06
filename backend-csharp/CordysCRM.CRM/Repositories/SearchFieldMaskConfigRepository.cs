using CordysCRM.CRM.Domain;
using Microsoft.EntityFrameworkCore;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// 搜索字段脱敏配置仓储实现 (Search Field Mask Config Repository Implementation)
/// Converted from Java SearchFieldMaskConfigMapper
/// </summary>
public class SearchFieldMaskConfigRepository : ISearchFieldMaskConfigRepository
{
    private readonly DbContext _context;

    public SearchFieldMaskConfigRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<SearchFieldMaskConfig?> GetByIdAsync(string id)
    {
        return await _context.Set<SearchFieldMaskConfig>()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<SearchFieldMaskConfig> AddAsync(SearchFieldMaskConfig config)
    {
        await _context.Set<SearchFieldMaskConfig>().AddAsync(config);
        await _context.SaveChangesAsync();
        return config;
    }

    public async Task<SearchFieldMaskConfig> UpdateAsync(SearchFieldMaskConfig config)
    {
        _context.Set<SearchFieldMaskConfig>().Update(config);
        await _context.SaveChangesAsync();
        return config;
    }

    public async Task DeleteAsync(string id)
    {
        var config = await GetByIdAsync(id);
        if (config != null)
        {
            _context.Set<SearchFieldMaskConfig>().Remove(config);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<SearchFieldMaskConfig>> GetByOrganizationIdAsync(string organizationId)
    {
        return await _context.Set<SearchFieldMaskConfig>()
            .Where(c => c.OrganizationId == organizationId)
            .ToListAsync();
    }
}
