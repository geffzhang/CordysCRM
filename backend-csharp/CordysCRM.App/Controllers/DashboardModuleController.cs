using CordysCRM.CRM.DTOs.Dashboard;
using CordysCRM.CRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace CordysCRM.App.Controllers;

/// <summary>
/// 仪表板模块 (Dashboard Module Controller)
/// Converted from Java Spring Boot to C# ASP.NET Core
/// </summary>
[ApiController]
[Route("dashboard/module")]
[Tags("仪表板模块")]
public class DashboardModuleController : ControllerBase
{
    private readonly ILogger<DashboardModuleController> _logger;
    private readonly IDashboardModuleService _dashboardModuleService;

    public DashboardModuleController(
        ILogger<DashboardModuleController> logger,
        IDashboardModuleService dashboardModuleService)
    {
        _logger = logger;
        _dashboardModuleService = dashboardModuleService;
    }

    /// <summary>
    /// 仪表板-添加文件夹 (Add Dashboard Folder)
    /// </summary>
    [HttpPost("add")]
    // [RequiresPermission(PermissionConstants.DashboardAdd)]
    public async Task<IActionResult> AddFileModule([FromBody] DashboardModuleAddRequest request)
    {
        _logger.LogInformation("AddFileModule called");
        var organizationId = "default-org"; // TODO: Get from session/context
        var userId = "default-user"; // TODO: Get from session/context
        await _dashboardModuleService.AddFileModuleAsync(request.Name, request.ParentId, organizationId, userId);
        return Ok();
    }

    /// <summary>
    /// 仪表板-重命名文件夹 (Rename Dashboard Folder)
    /// </summary>
    [HttpPost("rename")]
    // [RequiresPermission(PermissionConstants.DashboardEdit)]
    public async Task<IActionResult> Rename([FromBody] DashboardModuleRenameRequest request)
    {
        _logger.LogInformation("Rename called");
        var userId = "default-user"; // TODO: Get from session/context
        await _dashboardModuleService.RenameAsync(request.Id, request.NewName, userId);
        return Ok();
    }

    /// <summary>
    /// 仪表板-删除文件夹 (Delete Dashboard Folder)
    /// </summary>
    [HttpPost("delete")]
    // [RequiresPermission(PermissionConstants.DashboardDelete)]
    public async Task<IActionResult> DeleteDashboardModule([FromBody] List<string> ids)
    {
        _logger.LogInformation("DeleteDashboardModule called with {Count} ids", ids.Count);
        var userId = "default-user"; // TODO: Get from session/context
        var organizationId = "default-org"; // TODO: Get from session/context
        await _dashboardModuleService.DeleteAsync(ids, userId, organizationId);
        return Ok();
    }

    /// <summary>
    /// 仪表板-文件树查询 (Get Dashboard Tree)
    /// </summary>
    [HttpGet("tree")]
    // [RequiresPermission(PermissionConstants.DashboardRead)]
    public async Task<IActionResult> GetTree()
    {
        _logger.LogInformation("GetTree called");
        var userId = "default-user"; // TODO: Get from session/context
        var organizationId = "default-org"; // TODO: Get from session/context
        var tree = await _dashboardModuleService.GetTreeAsync(userId, organizationId);
        return Ok(tree);
    }

    /// <summary>
    /// 仪表板-文件树数量 (Get Module Count)
    /// </summary>
    [HttpGet("count")]
    // [RequiresPermission(PermissionConstants.DashboardRead)]
    public async Task<IActionResult> ModuleCount()
    {
        _logger.LogInformation("ModuleCount called");
        var userId = "default-user"; // TODO: Get from session/context
        var organizationId = "default-org"; // TODO: Get from session/context
        var count = await _dashboardModuleService.GetModuleCountAsync(userId, organizationId);
        return Ok(count);
    }

    /// <summary>
    /// 仪表板-文件夹移动 (Move Dashboard Folder)
    /// </summary>
    [HttpPost("move")]
    // [RequiresPermission(PermissionConstants.DashboardEdit)]
    public async Task<IActionResult> MoveNode([FromBody] DashboardModuleNodeMoveRequest request)
    {
        _logger.LogInformation("MoveNode called");
        var userId = "default-user"; // TODO: Get from session/context
        await _dashboardModuleService.MoveNodeAsync(request.Id, request.TargetId, userId);
        return Ok();
    }
}
