using CordysCRM.CRM.DTOs.Follow;
using CordysCRM.CRM.Services;
using CordysCRM.Framework.Security;
using Microsoft.AspNetCore.Mvc;

namespace CordysCRM.App.Controllers;

/// <summary>
/// 跟进计划统一页面 (Follow-Up Plan Controller)
/// Converted from Java FollowUpPlanController
/// </summary>
[ApiController]
[Route("follow/plan")]
[Tags("跟进计划统一页面")]
public class FollowUpPlanController : ControllerBase
{
    private readonly ILogger<FollowUpPlanController> _logger;
    private readonly IFollowUpPlanService _followUpPlanService;
    private readonly SessionUtils _sessionUtils;

    public FollowUpPlanController(
        ILogger<FollowUpPlanController> logger,
        IFollowUpPlanService followUpPlanService,
        SessionUtils sessionUtils)
    {
        _logger = logger;
        _followUpPlanService = followUpPlanService;
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
    /// 跟进记录列表 (Get Follow-Up Plan List)
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
        
        var plans = await _followUpPlanService.GetListAsync(user.OrganizationId, user.Id);
        return Ok(new { list = plans, total = plans.Count });
    }

    /// <summary>
    /// 删除跟进计划 (Delete Follow-Up Plan)
    /// </summary>
    [HttpGet("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        _logger.LogInformation("Delete called for plan");
        await _followUpPlanService.DeleteAsync(id);
        return Ok();
    }

    /// <summary>
    /// 更新跟进计划状态 (Update Follow-Up Plan Status)
    /// </summary>
    [HttpPost("status/update")]
    public async Task<IActionResult> UpdateStatus([FromBody] FollowUpPlanStatusRequest request)
    {
        var user = _sessionUtils.GetUser();
        if (user == null)
        {
            return Unauthorized(new { message = "User not authenticated" });
        }
        
        _logger.LogInformation("UpdateStatus called for plan");
        await _followUpPlanService.UpdateStatusAsync(request, user.Id);
        return Ok();
    }

    /// <summary>
    /// 跟进计划详情 (Get Follow-Up Plan Details)
    /// </summary>
    [HttpGet("get/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var user = _sessionUtils.GetUser();
        if (user == null)
        {
            return Unauthorized(new { message = "User not authenticated" });
        }
        
        _logger.LogInformation("Get called for plan");
        var plan = await _followUpPlanService.GetAsync(id, user.OrganizationId);
        return Ok(plan);
    }

    /// <summary>
    /// 更新线索跟进计划 (Update Follow-Up Plan)
    /// </summary>
    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] FollowUpPlanUpdateRequest request)
    {
        var user = _sessionUtils.GetUser();
        if (user == null)
        {
            return Unauthorized(new { message = "User not authenticated" });
        }
        
        _logger.LogInformation("Update called for plan");
        var plan = await _followUpPlanService.UpdateAsync(request, user.Id, user.OrganizationId);
        return Ok(plan);
    }

    /// <summary>
    /// 添加跟进计划 (Add Follow-Up Plan)
    /// </summary>
    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] FollowUpPlanAddRequest request)
    {
        var user = _sessionUtils.GetUser();
        if (user == null)
        {
            return Unauthorized(new { message = "User not authenticated" });
        }
        
        _logger.LogInformation("Add called");
        var plan = await _followUpPlanService.AddAsync(request, user.Id, user.OrganizationId);
        return Ok(plan);
    }
}
