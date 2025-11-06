using CordysCRM.CRM.Domain;
using CordysCRM.Framework.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// Clue repository implementation
/// </summary>
public class ClueRepository : Repository<Clue>, IClueRepository
{
    public ClueRepository(DbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Clue>> GetByOwnerAsync(
        string ownerId, 
        string organizationId, 
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(c => c.Owner == ownerId && c.OrganizationId == organizationId)
            .OrderByDescending(c => c.CreateTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Clue>> GetSharedPoolCluesAsync(
        string organizationId, 
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(c => c.InSharedPool == true && c.OrganizationId == organizationId)
            .OrderByDescending(c => c.CreateTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Clue>> GetByStageAsync(
        string stage, 
        string organizationId, 
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(c => c.Stage == stage && c.OrganizationId == organizationId)
            .ToListAsync(cancellationToken);
    }

    public async Task<(IEnumerable<Clue> Items, int TotalCount)> GetPagedAsync(
        int page, 
        int pageSize, 
        string? owner, 
        string? stage, 
        bool? inSharedPool, 
        string? searchKeyword, 
        string organizationId, 
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet.Where(c => c.OrganizationId == organizationId);

        if (!string.IsNullOrEmpty(owner))
        {
            query = query.Where(c => c.Owner == owner);
        }

        if (!string.IsNullOrEmpty(stage))
        {
            query = query.Where(c => c.Stage == stage);
        }

        if (inSharedPool.HasValue)
        {
            query = query.Where(c => c.InSharedPool == inSharedPool.Value);
        }

        if (!string.IsNullOrEmpty(searchKeyword))
        {
            query = query.Where(c => 
                (c.Name != null && c.Name.Contains(searchKeyword)) ||
                (c.Contact != null && c.Contact.Contains(searchKeyword)) ||
                (c.Phone != null && c.Phone.Contains(searchKeyword)));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(c => c.CreateTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }
}
