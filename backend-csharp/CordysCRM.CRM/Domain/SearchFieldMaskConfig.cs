using CordysCRM.Framework.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CordysCRM.CRM.Domain;

/// <summary>
/// 全局脱敏搜索配置 (Search Field Mask Config Entity)
/// Converted from Java SearchFieldMaskConfig domain model
/// </summary>
[Table("sys_search_field_mask_config")]
public class SearchFieldMaskConfig : BaseModel
{
    /// <summary>
    /// 搜索字段id (Field ID)
    /// </summary>
    [MaxLength(50)]
    public string? FieldId { get; set; }

    /// <summary>
    /// 类型 (Type)
    /// </summary>
    [MaxLength(50)]
    public string? Type { get; set; }

    /// <summary>
    /// 业务字段key (Business Key)
    /// </summary>
    [MaxLength(100)]
    public string? BusinessKey { get; set; }

    /// <summary>
    /// 数据源对应类型 (Data Source Type)
    /// </summary>
    [MaxLength(50)]
    public string? DataSourceType { get; set; }

    /// <summary>
    /// 搜索模块(customer/lead/等) (Module Type)
    /// </summary>
    [MaxLength(50)]
    public string? ModuleType { get; set; }

    /// <summary>
    /// 组织id (Organization ID)
    /// </summary>
    [MaxLength(50)]
    public string? OrganizationId { get; set; }
}
