using CordysCRM.CRM.DTOs.Opportunity;
using CordysCRM.CRM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CordysCRM.App.Controllers;

/// <summary>
/// 商机管理 (Opportunity Management Controller)
/// </summary>
[ApiController]
[Route("api/opportunities")]
[Tags("商机管理")]
public class OpportunityController : ControllerBase
{
    private readonly IOpportunityService _opportunityService;
    private readonly ILogger<OpportunityController> _logger;

    public OpportunityController(
        IOpportunityService opportunityService,
        ILogger<OpportunityController> logger)
    {
        _opportunityService = opportunityService ?? throw new ArgumentNullException(nameof(opportunityService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// 获取商机详情 (Get opportunity by ID)
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var opportunity = await _opportunityService.GetByIdAsync(id, cancellationToken);

        if (opportunity == null)
        {
            return NotFound(new { message = $"Opportunity with ID {id} not found" });
        }

        return Ok(opportunity);
    }

    /// <summary>
    /// 获取商机列表（分页） (Get opportunities with pagination)
    /// </summary>
    [HttpPost("list")]
    public async Task<IActionResult> GetList(
        [FromBody] OpportunityPageRequest request,
        [FromQuery] string organizationId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(organizationId))
        {
            return BadRequest(new { message = "Organization ID is required" });
        }

        var result = await _opportunityService.GetPagedAsync(request, organizationId, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// 创建商机 (Create new opportunity)
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] OpportunityAddRequest request,
        [FromQuery] string userId,
        [FromQuery] string organizationId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(organizationId))
        {
            return BadRequest(new { message = "User ID and Organization ID are required" });
        }

        var opportunity = await _opportunityService.CreateAsync(request, userId, organizationId, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = opportunity.Id },
            opportunity);
    }

    /// <summary>
    /// 更新商机 (Update opportunity)
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        string id,
        [FromBody] OpportunityUpdateRequest request,
        [FromQuery] string userId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest(new { message = "User ID is required" });
        }

        try
        {
            var opportunity = await _opportunityService.UpdateAsync(id, request, userId, cancellationToken);
            return Ok(opportunity);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = $"Opportunity with ID {id} not found" });
        }
    }

    /// <summary>
    /// 删除商机 (Delete opportunity)
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        try
        {
            await _opportunityService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = $"Opportunity with ID {id} not found" });
        }
    }

    /// <summary>
    /// 更新商机阶段 (Update opportunity stage)
    /// </summary>
    [HttpPut("{id}/stage")]
    public async Task<IActionResult> UpdateStage(
        string id,
        [FromBody] OpportunityStageRequest request,
        [FromQuery] string userId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest(new { message = "User ID is required" });
        }

        try
        {
            var opportunity = await _opportunityService.UpdateStageAsync(id, request, userId, cancellationToken);
            return Ok(opportunity);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = $"Opportunity with ID {id} not found" });
        }
    }

    /// <summary>
    /// 转移商机 (Transfer opportunities to another owner)
    /// </summary>
    [HttpPost("transfer")]
    public async Task<IActionResult> Transfer(
        [FromBody] OpportunityTransferRequest request,
        [FromQuery] string userId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest(new { message = "User ID is required" });
        }

        await _opportunityService.TransferAsync(request, userId, cancellationToken);
        return Ok(new { message = "Opportunities transferred successfully" });
    }

    /// <summary>
    /// 更新商机排序 (Update opportunity sort position)
    /// </summary>
    [HttpPut("sort")]
    public async Task<IActionResult> UpdatePosition(
        [FromBody] OpportunitySortRequest request,
        [FromQuery] string userId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest(new { message = "User ID is required" });
        }

        try
        {
            var opportunity = await _opportunityService.UpdatePositionAsync(request, userId, cancellationToken);
            return Ok(opportunity);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = $"Opportunity with ID {request.OpportunityId} not found" });
        }
    }

    /// <summary>
    /// 获取负责人的商机 (Get opportunities by owner)
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

        var opportunities = await _opportunityService.GetByOwnerAsync(ownerId, organizationId, cancellationToken);
        return Ok(opportunities);
    }

    /// <summary>
    /// 获取客户的商机 (Get opportunities by customer)
    /// </summary>
    [HttpGet("by-customer/{customerId}")]
    public async Task<IActionResult> GetByCustomer(
        string customerId,
        CancellationToken cancellationToken)
    {
        var opportunities = await _opportunityService.GetByCustomerAsync(customerId, cancellationToken);
        return Ok(opportunities);
    }

    /// <summary>
    /// 获取商机统计 (Get opportunity statistics)
    /// </summary>
    [HttpGet("statistics")]
    public async Task<IActionResult> GetStatistics(
        [FromQuery] string organizationId,
        [FromQuery] string? owner,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(organizationId))
        {
            return BadRequest(new { message = "Organization ID is required" });
        }

        var statistics = await _opportunityService.GetStatisticsAsync(organizationId, owner, cancellationToken);
        return Ok(statistics);
    }
}
