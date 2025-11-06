using CordysCRM.CRM.Domain;
using Microsoft.EntityFrameworkCore;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// 用户搜索配置仓储实现 (User Search Config Repository Implementation)
/// Converted from Java UserSearchConfigMapper
/// </summary>
public class UserSearchConfigRepository : IUserSearchConfigRepository
{
    private readonly DbContext _context;

    public UserSearchConfigRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<UserSearchConfig?> GetByIdAsync(string id)
    {
        return await _context.Set<UserSearchConfig>()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<UserSearchConfig> AddAsync(UserSearchConfig config)
    {
        await _context.Set<UserSearchConfig>().AddAsync(config);
        await _context.SaveChangesAsync();
        return config;
    }

    public async Task<UserSearchConfig> UpdateAsync(UserSearchConfig config)
    {
        _context.Set<UserSearchConfig>().Update(config);
        await _context.SaveChangesAsync();
        return config;
    }

    public async Task DeleteAsync(string id)
    {
        var config = await GetByIdAsync(id);
        if (config != null)
        {
            _context.Set<UserSearchConfig>().Remove(config);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<UserSearchConfig>> GetByUserIdAsync(string userId, string organizationId)
    {
        return await _context.Set<UserSearchConfig>()
            .Where(c => c.UserId == userId && c.OrganizationId == organizationId)
            .ToListAsync();
    }
}
