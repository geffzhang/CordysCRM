namespace CordysCRM.CRM.DTOs.Dashboard;

/// <summary>
/// 仪表板分页响应 (Dashboard Page Response)
/// Converted from Java DashboardPageResponse
/// </summary>
public class DashboardPageResponse
{
    /// <summary>
    /// ID
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// 名称 (Name)
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 仪表板url (Resource URL)
    /// </summary>
    public string? ResourceUrl { get; set; }

    /// <summary>
    /// 模块id (Dashboard Module ID)
    /// </summary>
    public string? DashboardModuleId { get; set; }

    /// <summary>
    /// 范围 (Scope ID)
    /// </summary>
    public string? ScopeId { get; set; }

    /// <summary>
    /// 描述 (Description)
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 创建人 (Create User)
    /// </summary>
    public string? CreateUser { get; set; }

    /// <summary>
    /// 创建时间 (Create Time)
    /// </summary>
    public long? CreateTime { get; set; }

    /// <summary>
    /// 是否收藏 (Is Collected)
    /// </summary>
    public bool IsCollected { get; set; } = false;
}
