using System.ComponentModel.DataAnnotations;

namespace CordysCRM.CRM.DTOs.Dashboard;

/// <summary>
/// 仪表板重命名请求 (Dashboard Rename Request)
/// Converted from Java DashboardRenameRequest
/// </summary>
public class DashboardRenameRequest
{
    /// <summary>
    /// ID
    /// </summary>
    [Required]
    public required string Id { get; set; }

    /// <summary>
    /// 新名称 (New Name)
    /// </summary>
    [Required]
    [MaxLength(255)]
    public required string NewName { get; set; }
}
