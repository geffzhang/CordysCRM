using CordysCRM.CRM.Domain;
using CordysCRM.Framework.Repositories;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// Opportunity repository interface
/// </summary>
public interface IOpportunityRepository : IRepository<Opportunity>
{
    /// <summary>
    /// Get opportunities by owner
    /// </summary>
    Task<IEnumerable<Opportunity>> GetByOwnerAsync(string ownerId, string organizationId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get opportunities by customer
    /// </summary>
    Task<IEnumerable<Opportunity>> GetByCustomerAsync(string customerId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get opportunities by stage
    /// </summary>
    Task<IEnumerable<Opportunity>> GetByStageAsync(string stage, string organizationId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get opportunities with pagination and filtering
    /// </summary>
    Task<(IEnumerable<Opportunity> Items, int TotalCount)> GetPagedAsync(
        int page,
        int pageSize,
        string? owner,
        string? stage,
        string? customerId,
        string? searchKeyword,
        decimal? minAmount,
        decimal? maxAmount,
        string organizationId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get opportunity statistics
    /// </summary>
    Task<(int TotalCount, decimal TotalAmount, Dictionary<string, int> StageDistribution, Dictionary<string, decimal> StageAmounts)> GetStatisticsAsync(
        string organizationId,
        string? owner = null,
        CancellationToken cancellationToken = default);
}
