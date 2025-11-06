using System.ComponentModel.DataAnnotations;

namespace CordysCRM.CRM.DTOs.Dashboard;

/// <summary>
/// 仪表板更新请求 (Dashboard Update Request)
/// Converted from Java DashboardUpdateRequest
/// </summary>
public class DashboardUpdateRequest
{
    /// <summary>
    /// ID
    /// </summary>
    [Required]
    public required string Id { get; set; }

    /// <summary>
    /// 仪表板名称 (Dashboard Name)
    /// </summary>
    [MaxLength(255)]
    public string? Name { get; set; }

    /// <summary>
    /// 仪表板url (Resource URL)
    /// </summary>
    public string? ResourceUrl { get; set; }

    /// <summary>
    /// 文件夹id (Dashboard Module ID)
    /// </summary>
    public string? DashboardModuleId { get; set; }

    /// <summary>
    /// 范围ID集合 (Scope IDs)
    /// </summary>
    public List<string>? ScopeIds { get; set; }

    /// <summary>
    /// 描述 (Description)
    /// </summary>
    public string? Description { get; set; }
}
