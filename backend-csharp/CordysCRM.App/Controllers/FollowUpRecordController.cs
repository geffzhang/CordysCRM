using CordysCRM.CRM.DTOs.Follow;
using CordysCRM.CRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace CordysCRM.App.Controllers;

/// <summary>
/// 跟进记录统一页面 (Follow-Up Record Controller)
/// Converted from Java FollowUpRecordController
/// </summary>
[ApiController]
[Route("follow/record")]
[Tags("跟进记录统一页面")]
public class FollowUpRecordController : ControllerBase
{
    private readonly ILogger<FollowUpRecordController> _logger;
    private readonly IFollowUpRecordService _followUpRecordService;

    public FollowUpRecordController(
        ILogger<FollowUpRecordController> logger,
        IFollowUpRecordService followUpRecordService)
    {
        _logger = logger;
        _followUpRecordService = followUpRecordService;
    }

    /// <summary>
    /// 获取表单配置 (Get Module Form Config)
    /// </summary>
    [HttpGet("module/form")]
    public IActionResult GetModuleFormConfig()
    {
        _logger.LogInformation("GetModuleFormConfig called");
        return Ok(new { message = "Not yet implemented" });
    }

    /// <summary>
    /// 数据权限TAB (Get Tab Enable Config)
    /// </summary>
    [HttpGet("tab")]
    public IActionResult GetTabEnableConfig()
    {
        _logger.LogInformation("GetTabEnableConfig called");
        return Ok(new { message = "Not yet implemented" });
    }

    /// <summary>
    /// 跟进记录列表 (Get Follow-Up Record List)
    /// </summary>
    [HttpPost("page")]
    public async Task<IActionResult> List([FromBody] object request)
    {
        _logger.LogInformation("List called");
        // TODO: Get organizationId and userId from session/context
        var organizationId = "default-org";
        var userId = "default-user";
        
        var records = await _followUpRecordService.GetListAsync(organizationId, userId);
        return Ok(new { list = records, total = records.Count });
    }

    /// <summary>
    /// 删除跟进记录 (Delete Follow-Up Record)
    /// </summary>
    [HttpGet("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        _logger.LogInformation("Delete called with id: {Id}", id);
        await _followUpRecordService.DeleteAsync(id);
        return Ok();
    }

    /// <summary>
    /// 跟进记录详情 (Get Follow-Up Record Details)
    /// </summary>
    [HttpGet("get/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        _logger.LogInformation("Get called with id: {Id}", id);
        var organizationId = "default-org"; // TODO: Get from session/context
        var record = await _followUpRecordService.GetAsync(id, organizationId);
        return Ok(record);
    }

    /// <summary>
    /// 更新跟进记录 (Update Follow-Up Record)
    /// </summary>
    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] FollowUpRecordUpdateRequest request)
    {
        _logger.LogInformation("Update called for id: {Id}", request.Id);
        var userId = "default-user"; // TODO: Get from session/context
        var organizationId = "default-org"; // TODO: Get from session/context
        var record = await _followUpRecordService.UpdateAsync(request, userId, organizationId);
        return Ok(record);
    }

    /// <summary>
    /// 添加跟进记录 (Add Follow-Up Record)
    /// </summary>
    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] FollowUpRecordAddRequest request)
    {
        _logger.LogInformation("Add called");
        var userId = "default-user"; // TODO: Get from session/context
        var organizationId = "default-org"; // TODO: Get from session/context
        var record = await _followUpRecordService.AddAsync(request, userId, organizationId);
        return Ok(record);
    }
}
