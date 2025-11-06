using CordysCRM.Framework.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CordysCRM.CRM.Domain;

/// <summary>
/// 仪表板模块 (Dashboard Module Entity)
/// Converted from Java DashboardModule domain model
/// </summary>
[Table("dashboard_module")]
public class DashboardModule : BaseModel
{
    /// <summary>
    /// 组织id (Organization ID)
    /// </summary>
    [MaxLength(50)]
    public string? OrganizationId { get; set; }

    /// <summary>
    /// 名称 (Name)
    /// </summary>
    [MaxLength(200)]
    public string? Name { get; set; }

    /// <summary>
    /// 父节点id (Parent ID)
    /// </summary>
    [MaxLength(50)]
    public string? ParentId { get; set; }

    /// <summary>
    /// 同一节点下顺序 (Position)
    /// </summary>
    public long? Pos { get; set; }
}
