using CordysCRM.Framework.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CordysCRM.CRM.Domain;

/// <summary>
/// 仪表板 (Dashboard Entity)
/// Converted from Java Dashboard domain model
/// </summary>
[Table("dashboard")]
public class Dashboard : BaseModel
{
    /// <summary>
    /// 名称 (Name)
    /// </summary>
    [MaxLength(200)]
    public string? Name { get; set; }

    /// <summary>
    /// 仪表板url (Resource URL)
    /// </summary>
    [MaxLength(500)]
    public string? ResourceUrl { get; set; }

    /// <summary>
    /// 模块id (Dashboard Module ID)
    /// </summary>
    [MaxLength(50)]
    public string? DashboardModuleId { get; set; }

    /// <summary>
    /// 组织id (Organization ID)
    /// </summary>
    [MaxLength(50)]
    public string? OrganizationId { get; set; }

    /// <summary>
    /// 同一节点下顺序 (Position)
    /// </summary>
    public long? Pos { get; set; }

    /// <summary>
    /// 范围 (Scope ID)
    /// </summary>
    [MaxLength(50)]
    public string? ScopeId { get; set; }

    /// <summary>
    /// 描述 (Description)
    /// </summary>
    [MaxLength(1000)]
    public string? Description { get; set; }
}
