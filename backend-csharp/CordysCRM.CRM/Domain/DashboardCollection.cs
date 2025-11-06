using CordysCRM.Framework.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CordysCRM.CRM.Domain;

/// <summary>
/// 仪表板收藏 (Dashboard Collection Entity)
/// Converted from Java DashboardCollection domain model
/// </summary>
[Table("dashboard_collection")]
public class DashboardCollection : BaseModel
{
    /// <summary>
    /// 仪表板id (Dashboard ID)
    /// </summary>
    [MaxLength(50)]
    public string? DashboardId { get; set; }

    /// <summary>
    /// 用户id (User ID)
    /// </summary>
    [MaxLength(50)]
    public string? UserId { get; set; }
}
