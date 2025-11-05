using CordysCRM.CRM.Domain;
using CordysCRM.Framework.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// Customer repository implementation
/// </summary>
public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(DbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Customer>> GetByPoolIdAsync(string poolId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(c => c.PoolId == poolId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Customer>> GetPendingFollowUpAsync(
        string organizationId, 
        long beforeTime, 
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(c => c.OrganizationId == organizationId && 
                       c.FollowTime < beforeTime &&
                       c.InSharedPool == false)
            .OrderBy(c => c.FollowTime)
            .ToListAsync(cancellationToken);
    }
}
