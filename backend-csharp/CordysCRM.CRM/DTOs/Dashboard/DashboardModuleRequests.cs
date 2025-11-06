namespace CordysCRM.CRM.DTOs.Dashboard;

/// <summary>
/// 仪表板模块添加请求 (Dashboard Module Add Request)
/// </summary>
public record DashboardModuleAddRequest(string Name, string? ParentId);

/// <summary>
/// 仪表板模块重命名请求 (Dashboard Module Rename Request)
/// </summary>
public record DashboardModuleRenameRequest(string Id, string NewName);

/// <summary>
/// 仪表板模块节点移动请求 (Dashboard Module Node Move Request)
/// </summary>
public record DashboardModuleNodeMoveRequest(string Id, string? TargetId);
