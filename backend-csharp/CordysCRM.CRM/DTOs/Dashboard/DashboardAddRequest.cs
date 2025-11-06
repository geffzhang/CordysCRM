using System.ComponentModel.DataAnnotations;

namespace CordysCRM.CRM.DTOs.Dashboard;

/// <summary>
/// 仪表板添加请求 (Dashboard Add Request)
/// Converted from Java DashboardAddRequest
/// </summary>
public class DashboardAddRequest
{
    /// <summary>
    /// 仪表板名称 (Dashboard Name)
    /// </summary>
    [Required]
    [MaxLength(255)]
    public required string Name { get; set; }

    /// <summary>
    /// 仪表板url (Resource URL)
    /// </summary>
    [Required]
    public required string ResourceUrl { get; set; }

    /// <summary>
    /// 文件夹id (Dashboard Module ID)
    /// </summary>
    [Required]
    public required string DashboardModuleId { get; set; }

    /// <summary>
    /// 范围ID集合 (Scope IDs)
    /// </summary>
    [Required]
    public required List<string> ScopeIds { get; set; }

    /// <summary>
    /// 描述 (Description)
    /// </summary>
    public string? Description { get; set; }
}
