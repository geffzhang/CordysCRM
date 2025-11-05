using CordysCRM.CRM.Domain;
using CordysCRM.CRM.DTOs;
using CordysCRM.CRM.Repositories;
using Microsoft.Extensions.Logging;

namespace CordysCRM.CRM.Services;

/// <summary>
/// Customer service implementation
/// </summary>
public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<CustomerService> _logger;

    public CustomerService(
        ICustomerRepository customerRepository,
        ILogger<CustomerService> logger)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Customer?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _customerRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Customer>> GetAllAsync(string organizationId, CancellationToken cancellationToken = default)
    {
        return await _customerRepository.FindAsync(
            c => c.OrganizationId == organizationId, 
            cancellationToken);
    }

    public async Task<Customer> CreateAsync(
        CustomerCreateDto dto, 
        string userId, 
        string organizationId, 
        CancellationToken cancellationToken = default)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name,
            Owner = dto.Owner,
            PoolId = dto.PoolId,
            InSharedPool = dto.InSharedPool,
            OrganizationId = organizationId,
            CreateUser = userId,
            UpdateUser = userId,
            CreateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            UpdateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };

        _logger.LogInformation("Creating customer: {Name} for organization: {OrgId}", dto.Name, organizationId);
        
        return await _customerRepository.AddAsync(customer, cancellationToken);
    }

    public async Task<Customer> UpdateAsync(
        string id, 
        CustomerUpdateDto dto, 
        string userId, 
        CancellationToken cancellationToken = default)
    {
        var customer = await _customerRepository.GetByIdAsync(id, cancellationToken);
        
        if (customer == null)
        {
            throw new KeyNotFoundException($"Customer with ID {id} not found");
        }

        // Update fields
        if (dto.Name != null) customer.Name = dto.Name;
        if (dto.Owner != null) customer.Owner = dto.Owner;
        if (dto.PoolId != null) customer.PoolId = dto.PoolId;
        if (dto.InSharedPool.HasValue) customer.InSharedPool = dto.InSharedPool;
        if (dto.Follower != null) customer.Follower = dto.Follower;
        if (dto.FollowTime.HasValue) customer.FollowTime = dto.FollowTime;

        customer.UpdateUser = userId;
        customer.UpdateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        _logger.LogInformation("Updating customer: {Id}", id);

        await _customerRepository.UpdateAsync(customer, cancellationToken);
        return customer;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting customer: {Id}", id);
        await _customerRepository.DeleteByIdAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Customer>> GetByOwnerAsync(
        string ownerId, 
        string organizationId, 
        CancellationToken cancellationToken = default)
    {
        return await _customerRepository.FindAsync(
            c => c.Owner == ownerId && c.OrganizationId == organizationId,
            cancellationToken);
    }

    public async Task<IEnumerable<Customer>> GetSharedPoolCustomersAsync(
        string organizationId, 
        CancellationToken cancellationToken = default)
    {
        return await _customerRepository.FindAsync(
            c => c.InSharedPool == true && c.OrganizationId == organizationId,
            cancellationToken);
    }
}
