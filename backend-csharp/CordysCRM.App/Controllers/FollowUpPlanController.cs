using CordysCRM.CRM.DTOs.Follow;
using CordysCRM.CRM.Services;
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

    public FollowUpPlanController(
        ILogger<FollowUpPlanController> logger,
        IFollowUpPlanService followUpPlanService)
    {
        _logger = logger;
        _followUpPlanService = followUpPlanService;
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
        _logger.LogInformation("List called");
        // TODO: Get organizationId and userId from session/context
        var organizationId = "default-org";
        var userId = "default-user";
        
        var plans = await _followUpPlanService.GetListAsync(organizationId, userId);
        return Ok(new { list = plans, total = plans.Count });
    }

    /// <summary>
    /// 删除跟进计划 (Delete Follow-Up Plan)
    /// </summary>
    [HttpGet("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        _logger.LogInformation("Delete called with id: {Id}", id);
        await _followUpPlanService.DeleteAsync(id);
        return Ok();
    }

    /// <summary>
    /// 更新跟进计划状态 (Update Follow-Up Plan Status)
    /// </summary>
    [HttpPost("status/update")]
    public async Task<IActionResult> UpdateStatus([FromBody] FollowUpPlanStatusRequest request)
    {
        _logger.LogInformation("UpdateStatus called for id: {Id}", request.Id);
        var userId = "default-user"; // TODO: Get from session/context
        await _followUpPlanService.UpdateStatusAsync(request, userId);
        return Ok();
    }

    /// <summary>
    /// 跟进计划详情 (Get Follow-Up Plan Details)
    /// </summary>
    [HttpGet("get/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        _logger.LogInformation("Get called with id: {Id}", id);
        var organizationId = "default-org"; // TODO: Get from session/context
        var plan = await _followUpPlanService.GetAsync(id, organizationId);
        return Ok(plan);
    }

    /// <summary>
    /// 更新线索跟进计划 (Update Follow-Up Plan)
    /// </summary>
    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] FollowUpPlanUpdateRequest request)
    {
        _logger.LogInformation("Update called for id: {Id}", request.Id);
        var userId = "default-user"; // TODO: Get from session/context
        var organizationId = "default-org"; // TODO: Get from session/context
        var plan = await _followUpPlanService.UpdateAsync(request, userId, organizationId);
        return Ok(plan);
    }

    /// <summary>
    /// 添加跟进计划 (Add Follow-Up Plan)
    /// </summary>
    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] FollowUpPlanAddRequest request)
    {
        _logger.LogInformation("Add called");
        var userId = "default-user"; // TODO: Get from session/context
        var organizationId = "default-org"; // TODO: Get from session/context
        var plan = await _followUpPlanService.AddAsync(request, userId, organizationId);
        return Ok(plan);
    }
}
