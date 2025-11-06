using CordysCRM.CRM.Domain;
using CordysCRM.CRM.DTOs.Opportunity;

namespace CordysCRM.CRM.Services;

/// <summary>
/// Opportunity service interface
/// </summary>
public interface IOpportunityService
{
    /// <summary>
    /// Get opportunity by ID
    /// </summary>
    Task<Opportunity?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get opportunities with pagination
    /// </summary>
    Task<OpportunityListResponse> GetPagedAsync(OpportunityPageRequest request, string organizationId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create new opportunity
    /// </summary>
    Task<Opportunity> CreateAsync(OpportunityAddRequest request, string userId, string organizationId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update opportunity
    /// </summary>
    Task<Opportunity> UpdateAsync(string id, OpportunityUpdateRequest request, string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete opportunity
    /// </summary>
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update opportunity stage
    /// </summary>
    Task<Opportunity> UpdateStageAsync(string id, OpportunityStageRequest request, string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Transfer opportunities to another owner
    /// </summary>
    Task TransferAsync(OpportunityTransferRequest request, string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update opportunity sort position
    /// </summary>
    Task<Opportunity> UpdatePositionAsync(OpportunitySortRequest request, string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get opportunities by owner
    /// </summary>
    Task<IEnumerable<Opportunity>> GetByOwnerAsync(string ownerId, string organizationId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get opportunities by customer
    /// </summary>
    Task<IEnumerable<Opportunity>> GetByCustomerAsync(string customerId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get opportunity statistics
    /// </summary>
    Task<OpportunityStatisticsResponse> GetStatisticsAsync(string organizationId, string? owner = null, CancellationToken cancellationToken = default);
}
