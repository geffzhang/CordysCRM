using CordysCRM.CRM.DTOs.Dashboard;
using CordysCRM.CRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace CordysCRM.App.Controllers;

/// <summary>
/// 仪表板 (Dashboard Controller)
/// Converted from Java DashboardController
/// </summary>
[ApiController]
[Route("dashboard")]
[Tags("仪表板")]
public class DashboardController : ControllerBase
{
    private readonly ILogger<DashboardController> _logger;
    private readonly IDashboardService _dashboardService;

    public DashboardController(
        ILogger<DashboardController> logger,
        IDashboardService dashboardService)
    {
        _logger = logger;
        _dashboardService = dashboardService;
    }

    /// <summary>
    /// 仪表板-添加 (Add Dashboard)
    /// </summary>
    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] DashboardAddRequest request)
    {
        _logger.LogInformation("Add Dashboard called");
        var organizationId = "default-org"; // TODO: Get from session/context
        var userId = "default-user"; // TODO: Get from session/context
        var dashboard = await _dashboardService.AddAsync(request, organizationId, userId);
        return Ok(dashboard);
    }

    /// <summary>
    /// 仪表板-详情 (Get Dashboard Detail)
    /// </summary>
    [HttpGet("detail/{id}")]
    public async Task<IActionResult> Detail(string id)
    {
        _logger.LogInformation("Get Dashboard Detail called with id: {Id}", id);
        var dashboard = await _dashboardService.GetAsync(id);
        return Ok(dashboard);
    }

    /// <summary>
    /// 仪表板-更新 (Update Dashboard)
    /// </summary>
    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] DashboardUpdateRequest request)
    {
        _logger.LogInformation("Update Dashboard called for id: {Id}", request.Id);
        var organizationId = "default-org"; // TODO: Get from session/context
        var userId = "default-user"; // TODO: Get from session/context
        await _dashboardService.UpdateAsync(request, organizationId, userId);
        return Ok();
    }

    /// <summary>
    /// 仪表板-重命名 (Rename Dashboard)
    /// </summary>
    [HttpPost("rename")]
    public async Task<IActionResult> Rename([FromBody] DashboardRenameRequest request)
    {
        _logger.LogInformation("Rename Dashboard called for id: {Id}", request.Id);
        var userId = "default-user"; // TODO: Get from session/context
        var organizationId = "default-org"; // TODO: Get from session/context
        await _dashboardService.RenameAsync(request, userId, organizationId);
        return Ok();
    }

    /// <summary>
    /// 仪表板-删除 (Delete Dashboard)
    /// </summary>
    [HttpGet("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        _logger.LogInformation("Delete Dashboard called with id: {Id}", id);
        await _dashboardService.DeleteAsync(id);
        return Ok();
    }

    /// <summary>
    /// 仪表板列表 (Get Dashboard List)
    /// </summary>
    [HttpPost("page")]
    public async Task<IActionResult> List()
    {
        _logger.LogInformation("Get Dashboard List called");
        var organizationId = "default-org"; // TODO: Get from session/context
        var dashboards = await _dashboardService.GetListAsync(organizationId);
        return Ok(new { list = dashboards, total = dashboards.Count });
    }

    /// <summary>
    /// 仪表板收藏 (Collect Dashboard)
    /// </summary>
    [HttpGet("collect/{id}")]
    public async Task<IActionResult> Collect(string id)
    {
        _logger.LogInformation("Collect Dashboard called with id: {Id}", id);
        var userId = "default-user"; // TODO: Get from session/context
        await _dashboardService.CollectAsync(id, userId);
        return Ok();
    }

    /// <summary>
    /// 仪表板取消收藏 (Un-collect Dashboard)
    /// </summary>
    [HttpGet("un-collect/{id}")]
    public async Task<IActionResult> UnCollect(string id)
    {
        _logger.LogInformation("Un-collect Dashboard called with id: {Id}", id);
        var userId = "default-user"; // TODO: Get from session/context
        await _dashboardService.UnCollectAsync(id, userId);
        return Ok();
    }
}
