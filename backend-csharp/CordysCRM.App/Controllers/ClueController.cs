using CordysCRM.CRM.DTOs.Clue;
using CordysCRM.CRM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CordysCRM.App.Controllers;

/// <summary>
/// 线索管理 (Clue/Lead Management Controller)
/// </summary>
[ApiController]
[Route("api/clues")]
[Tags("线索管理")]
public class ClueController : ControllerBase
{
    private readonly IClueService _clueService;
    private readonly ILogger<ClueController> _logger;

    public ClueController(
        IClueService clueService,
        ILogger<ClueController> logger)
    {
        _clueService = clueService ?? throw new ArgumentNullException(nameof(clueService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// 获取线索详情 (Get clue by ID)
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var clue = await _clueService.GetByIdAsync(id, cancellationToken);
        
        if (clue == null)
        {
            return NotFound(new { message = $"Clue with ID {id} not found" });
        }

        return Ok(clue);
    }

    /// <summary>
    /// 获取线索列表（分页） (Get clues with pagination)
    /// </summary>
    [HttpPost("list")]
    public async Task<IActionResult> GetList(
        [FromBody] CluePageRequest request,
        [FromQuery] string organizationId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(organizationId))
        {
            return BadRequest(new { message = "Organization ID is required" });
        }

        var result = await _clueService.GetPagedAsync(request, organizationId, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// 创建线索 (Create new clue)
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] ClueAddRequest request,
        [FromQuery] string userId,
        [FromQuery] string organizationId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(organizationId))
        {
            return BadRequest(new { message = "User ID and Organization ID are required" });
        }

        var clue = await _clueService.CreateAsync(request, userId, organizationId, cancellationToken);
        
        return CreatedAtAction(
            nameof(GetById),
            new { id = clue.Id },
            clue);
    }

    /// <summary>
    /// 更新线索 (Update clue)
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        string id,
        [FromBody] ClueUpdateRequest request,
        [FromQuery] string userId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest(new { message = "User ID is required" });
        }

        try
        {
            var clue = await _clueService.UpdateAsync(id, request, userId, cancellationToken);
            return Ok(clue);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = $"Clue with ID {id} not found" });
        }
    }

    /// <summary>
    /// 删除线索 (Delete clue)
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        try
        {
            await _clueService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = $"Clue with ID {id} not found" });
        }
    }

    /// <summary>
    /// 更新线索状态 (Update clue status/stage)
    /// </summary>
    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(
        string id,
        [FromBody] ClueStatusUpdateRequest request,
        [FromQuery] string userId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest(new { message = "User ID is required" });
        }

        try
        {
            var clue = await _clueService.UpdateStatusAsync(id, request, userId, cancellationToken);
            return Ok(clue);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = $"Clue with ID {id} not found" });
        }
    }

    /// <summary>
    /// 转移线索 (Transfer clue to another owner)
    /// </summary>
    [HttpPut("{id}/transfer")]
    public async Task<IActionResult> Transfer(
        string id,
        [FromQuery] string newOwnerId,
        [FromQuery] string userId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(newOwnerId) || string.IsNullOrEmpty(userId))
        {
            return BadRequest(new { message = "New Owner ID and User ID are required" });
        }

        try
        {
            var clue = await _clueService.TransferAsync(id, newOwnerId, userId, cancellationToken);
            return Ok(clue);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = $"Clue with ID {id} not found" });
        }
    }

    /// <summary>
    /// 批量转移线索 (Batch transfer clues)
    /// </summary>
    [HttpPost("batch-transfer")]
    public async Task<IActionResult> BatchTransfer(
        [FromBody] ClueBatchTransferRequest request,
        [FromQuery] string userId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest(new { message = "User ID is required" });
        }

        await _clueService.BatchTransferAsync(request, userId, cancellationToken);
        return Ok(new { message = "Clues transferred successfully" });
    }

    /// <summary>
    /// 获取负责人的线索 (Get clues by owner)
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

        var clues = await _clueService.GetByOwnerAsync(ownerId, organizationId, cancellationToken);
        return Ok(clues);
    }

    /// <summary>
    /// 获取公海线索 (Get shared pool clues)
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

        var clues = await _clueService.GetSharedPoolCluesAsync(organizationId, cancellationToken);
        return Ok(clues);
    }
}
