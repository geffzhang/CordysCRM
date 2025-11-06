using CordysCRM.CRM.Domain;
using CordysCRM.CRM.DTOs;

namespace CordysCRM.CRM.Services;

/// <summary>
/// Customer service interface
/// </summary>
public interface ICustomerService
{
    /// <summary>
    /// Get customer by ID
    /// </summary>
    Task<Customer?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all customers for organization
    /// </summary>
    Task<IEnumerable<Customer>> GetAllAsync(string organizationId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create new customer
    /// </summary>
    Task<Customer> CreateAsync(CustomerCreateDto dto, string userId, string organizationId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update customer
    /// </summary>
    Task<Customer> UpdateAsync(string id, CustomerUpdateDto dto, string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete customer
    /// </summary>
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get customers by owner
    /// </summary>
    Task<IEnumerable<Customer>> GetByOwnerAsync(string ownerId, string organizationId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get customers in shared pool
    /// </summary>
    Task<IEnumerable<Customer>> GetSharedPoolCustomersAsync(string organizationId, CancellationToken cancellationToken = default);
}
