using CordysCRM.CRM.Domain;
using CordysCRM.Framework.Repositories;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// Customer repository interface
/// </summary>
public interface ICustomerRepository : IRepository<Customer>
{
    /// <summary>
    /// Get customers by pool ID
    /// </summary>
    Task<IEnumerable<Customer>> GetByPoolIdAsync(string poolId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get customers that need follow-up
    /// </summary>
    Task<IEnumerable<Customer>> GetPendingFollowUpAsync(string organizationId, long beforeTime, CancellationToken cancellationToken = default);
}
