using CordysCRM.CRM.Domain;
using CordysCRM.Framework.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// Opportunity repository implementation
/// </summary>
public class OpportunityRepository : Repository<Opportunity>, IOpportunityRepository
{
    public OpportunityRepository(DbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Opportunity>> GetByOwnerAsync(
        string ownerId,
        string organizationId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(o => o.Owner == ownerId && o.OrganizationId == organizationId)
            .OrderByDescending(o => o.CreateTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Opportunity>> GetByCustomerAsync(
        string customerId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(o => o.CustomerId == customerId)
            .OrderByDescending(o => o.CreateTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Opportunity>> GetByStageAsync(
        string stage,
        string organizationId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(o => o.Stage == stage && o.OrganizationId == organizationId)
            .OrderBy(o => o.Pos)
            .ThenByDescending(o => o.CreateTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<(IEnumerable<Opportunity> Items, int TotalCount)> GetPagedAsync(
        int page,
        int pageSize,
        string? owner,
        string? stage,
        string? customerId,
        string? searchKeyword,
        decimal? minAmount,
        decimal? maxAmount,
        string organizationId,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet.Where(o => o.OrganizationId == organizationId);

        if (!string.IsNullOrEmpty(owner))
        {
            query = query.Where(o => o.Owner == owner);
        }

        if (!string.IsNullOrEmpty(stage))
        {
            query = query.Where(o => o.Stage == stage);
        }

        if (!string.IsNullOrEmpty(customerId))
        {
            query = query.Where(o => o.CustomerId == customerId);
        }

        if (!string.IsNullOrEmpty(searchKeyword))
        {
            query = query.Where(o =>
                (o.Name != null && o.Name.Contains(searchKeyword)) ||
                (o.FailureReason != null && o.FailureReason.Contains(searchKeyword)));
        }

        if (minAmount.HasValue)
        {
            query = query.Where(o => o.Amount >= minAmount.Value);
        }

        if (maxAmount.HasValue)
        {
            query = query.Where(o => o.Amount <= maxAmount.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(o => o.Pos)
            .ThenByDescending(o => o.CreateTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task<(int TotalCount, decimal TotalAmount, Dictionary<string, int> StageDistribution, Dictionary<string, decimal> StageAmounts)> GetStatisticsAsync(
        string organizationId,
        string? owner = null,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet.Where(o => o.OrganizationId == organizationId);

        if (!string.IsNullOrEmpty(owner))
        {
            query = query.Where(o => o.Owner == owner);
        }

        var opportunities = await query.ToListAsync(cancellationToken);

        var totalCount = opportunities.Count;
        var totalAmount = opportunities.Sum(o => o.Amount ?? 0);

        var stageDistribution = opportunities
            .GroupBy(o => o.Stage ?? "未知")
            .ToDictionary(g => g.Key, g => g.Count());

        var stageAmounts = opportunities
            .GroupBy(o => o.Stage ?? "未知")
            .ToDictionary(g => g.Key, g => g.Sum(o => o.Amount ?? 0));

        return (totalCount, totalAmount, stageDistribution, stageAmounts);
    }
}
