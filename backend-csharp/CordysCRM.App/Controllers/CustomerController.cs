using CordysCRM.CRM.DTOs;
using CordysCRM.CRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace CordysCRM.App.Controllers;

/// <summary>
/// 客户管理 (Customer Management Controller)
/// </summary>
[ApiController]
[Route("api/customers")]
[Tags("客户管理")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly ILogger<CustomerController> _logger;

    public CustomerController(
        ICustomerService customerService,
        ILogger<CustomerController> logger)
    {
        _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// 获取客户详情 (Get customer by ID)
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var customer = await _customerService.GetByIdAsync(id, cancellationToken);
        
        if (customer == null)
        {
            return NotFound(new { message = $"Customer with ID {id} not found" });
        }

        return Ok(customer);
    }

    /// <summary>
    /// 获取组织的所有客户 (Get all customers for organization)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string organizationId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(organizationId))
        {
            return BadRequest(new { message = "Organization ID is required" });
        }

        var customers = await _customerService.GetAllAsync(organizationId, cancellationToken);
        return Ok(customers);
    }

    /// <summary>
    /// 创建客户 (Create new customer)
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CustomerCreateDto dto, 
        [FromQuery] string userId,
        [FromQuery] string organizationId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(organizationId))
        {
            return BadRequest(new { message = "User ID and Organization ID are required" });
        }

        var customer = await _customerService.CreateAsync(dto, userId, organizationId, cancellationToken);
        
        return CreatedAtAction(
            nameof(GetById), 
            new { id = customer.Id }, 
            customer);
    }

    /// <summary>
    /// 更新客户 (Update customer)
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        string id, 
        [FromBody] CustomerUpdateDto dto,
        [FromQuery] string userId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest(new { message = "User ID is required" });
        }

        try
        {
            var customer = await _customerService.UpdateAsync(id, dto, userId, cancellationToken);
            return Ok(customer);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = $"Customer with ID {id} not found" });
        }
    }

    /// <summary>
    /// 删除客户 (Delete customer)
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        try
        {
            await _customerService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = $"Customer with ID {id} not found" });
        }
    }

    /// <summary>
    /// 获取负责人的客户 (Get customers by owner)
    /// </summary>
    [HttpGet("by-owner/{ownerId}")]
    public async Task<IActionResult> GetByOwner(
        string ownerId,
        [FromQuery] string organizationId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(organizationId))
        {
            return BadRequest(new { message = "Organization ID is required" });
        }

        var customers = await _customerService.GetByOwnerAsync(ownerId, organizationId, cancellationToken);
        return Ok(customers);
    }

    /// <summary>
    /// 获取公海客户 (Get shared pool customers)
    /// </summary>
    [HttpGet("shared-pool")]
    public async Task<IActionResult> GetSharedPool(
        [FromQuery] string organizationId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(organizationId))
        {
            return BadRequest(new { message = "Organization ID is required" });
        }

        var customers = await _customerService.GetSharedPoolCustomersAsync(organizationId, cancellationToken);
        return Ok(customers);
    }
}
