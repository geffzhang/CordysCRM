using CordysCRM.CRM.DTOs.Follow;
using CordysCRM.CRM.Services;
using CordysCRM.Framework.Security;
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
    private readonly SessionUtils _sessionUtils;

    public FollowUpRecordController(
        ILogger<FollowUpRecordController> logger,
        IFollowUpRecordService followUpRecordService,
        SessionUtils sessionUtils)
    {
        _logger = logger;
        _followUpRecordService = followUpRecordService;
        _sessionUtils = sessionUtils;
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
        var user = _sessionUtils.GetUser();
        if (user == null)
        {
            return Unauthorized(new { message = "User not authenticated" });
        }
        
        _logger.LogInformation("List called for user {UserId}", user.Id);
        
        var records = await _followUpRecordService.GetListAsync(user.OrganizationId, user.Id);
        return Ok(new { list = records, total = records.Count });
    }

    /// <summary>
    /// 删除跟进记录 (Delete Follow-Up Record)
    /// </summary>
    [HttpGet("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        _logger.LogInformation("Delete called for record");
        await _followUpRecordService.DeleteAsync(id);
        return Ok();
    }

    /// <summary>
    /// 跟进记录详情 (Get Follow-Up Record Details)
    /// </summary>
    [HttpGet("get/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var user = _sessionUtils.GetUser();
        if (user == null)
        {
            return Unauthorized(new { message = "User not authenticated" });
        }
        
        _logger.LogInformation("Get called for record");
        var record = await _followUpRecordService.GetAsync(id, user.OrganizationId);
        return Ok(record);
    }

    /// <summary>
    /// 更新跟进记录 (Update Follow-Up Record)
    /// </summary>
    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] FollowUpRecordUpdateRequest request)
    {
        var user = _sessionUtils.GetUser();
        if (user == null)
        {
            return Unauthorized(new { message = "User not authenticated" });
        }
        
        _logger.LogInformation("Update called for record");
        var record = await _followUpRecordService.UpdateAsync(request, user.Id, user.OrganizationId);
        return Ok(record);
    }

    /// <summary>
    /// 添加跟进记录 (Add Follow-Up Record)
    /// </summary>
    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] FollowUpRecordAddRequest request)
    {
        var user = _sessionUtils.GetUser();
        if (user == null)
        {
            return Unauthorized(new { message = "User not authenticated" });
        }
        
        _logger.LogInformation("Add called");
        var record = await _followUpRecordService.AddAsync(request, user.Id, user.OrganizationId);
        return Ok(record);
    }
}
