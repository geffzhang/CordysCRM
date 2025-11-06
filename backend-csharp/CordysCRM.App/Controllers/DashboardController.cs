using CordysCRM.CRM.DTOs.Dashboard;
using CordysCRM.CRM.Services;
using CordysCRM.Framework.Security;
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
    private readonly SessionUtils _sessionUtils;

    public DashboardController(
        ILogger<DashboardController> logger,
        IDashboardService dashboardService,
        SessionUtils sessionUtils)
    {
        _logger = logger;
        _dashboardService = dashboardService;
        _sessionUtils = sessionUtils;
    }

    /// <summary>
    /// 仪表板-添加 (Add Dashboard)
    /// </summary>
    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] DashboardAddRequest request)
    {
        var user = _sessionUtils.GetUser();
        if (user == null)
        {
            return Unauthorized(new { message = "User not authenticated" });
        }
        
        _logger.LogInformation("Add Dashboard called");
        var dashboard = await _dashboardService.AddAsync(request, user.OrganizationId, user.Id);
        return Ok(dashboard);
    }

    /// <summary>
    /// 仪表板-详情 (Get Dashboard Detail)
    /// </summary>
    [HttpGet("detail/{id}")]
    public async Task<IActionResult> Detail(string id)
    {
        _logger.LogInformation("Get Dashboard Detail called");
        var dashboard = await _dashboardService.GetAsync(id);
        return Ok(dashboard);
    }

    /// <summary>
    /// 仪表板-更新 (Update Dashboard)
    /// </summary>
    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] DashboardUpdateRequest request)
    {
        var user = _sessionUtils.GetUser();
        if (user == null)
        {
            return Unauthorized(new { message = "User not authenticated" });
        }
        
        _logger.LogInformation("Update Dashboard called");
        await _dashboardService.UpdateAsync(request, user.OrganizationId, user.Id);
        return Ok();
    }

    /// <summary>
    /// 仪表板-重命名 (Rename Dashboard)
    /// </summary>
    [HttpPost("rename")]
    public async Task<IActionResult> Rename([FromBody] DashboardRenameRequest request)
    {
        var user = _sessionUtils.GetUser();
        if (user == null)
        {
            return Unauthorized(new { message = "User not authenticated" });
        }
        
        _logger.LogInformation("Rename Dashboard called");
        await _dashboardService.RenameAsync(request, user.Id, user.OrganizationId);
        return Ok();
    }

    /// <summary>
    /// 仪表板-删除 (Delete Dashboard)
    /// </summary>
    [HttpGet("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        _logger.LogInformation("Delete Dashboard called");
        await _dashboardService.DeleteAsync(id);
        return Ok();
    }

    /// <summary>
    /// 仪表板列表 (Get Dashboard List)
    /// </summary>
    [HttpPost("page")]
    public async Task<IActionResult> List()
    {
        var user = _sessionUtils.GetUser();
        if (user == null)
        {
            return Unauthorized(new { message = "User not authenticated" });
        }
        
        _logger.LogInformation("Get Dashboard List called");
        var dashboards = await _dashboardService.GetListAsync(user.OrganizationId, user.Id);
        return Ok(new { list = dashboards, total = dashboards.Count });
    }

    /// <summary>
    /// 仪表板收藏 (Collect Dashboard)
    /// </summary>
    [HttpGet("collect/{id}")]
    public async Task<IActionResult> Collect(string id)
    {
        var user = _sessionUtils.GetUser();
        if (user == null)
        {
            return Unauthorized(new { message = "User not authenticated" });
        }
        
        _logger.LogInformation("Collect Dashboard called");
        await _dashboardService.CollectAsync(id, user.Id);
        return Ok();
    }

    /// <summary>
    /// 仪表板取消收藏 (Un-collect Dashboard)
    /// </summary>
    [HttpGet("un-collect/{id}")]
    public async Task<IActionResult> UnCollect(string id)
    {
        var user = _sessionUtils.GetUser();
        if (user == null)
        {
            return Unauthorized(new { message = "User not authenticated" });
        }
        
        _logger.LogInformation("Un-collect Dashboard called");
        await _dashboardService.UnCollectAsync(id, user.Id);
        return Ok();
    }
}
