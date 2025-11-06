using CordysCRM.Framework.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CordysCRM.CRM.Domain;

/// <summary>
/// 用户全局搜索范围 (User Search Config Entity)
/// Converted from Java UserSearchConfig domain model
/// </summary>
[Table("sys_user_search_config")]
public class UserSearchConfig : BaseModel
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
    /// 用户id (User ID)
    /// </summary>
    [MaxLength(50)]
    public string? UserId { get; set; }

    /// <summary>
    /// 搜索模块(customer/lead/等) (Module Type)
    /// </summary>
    [MaxLength(50)]
    public string? ModuleType { get; set; }

    /// <summary>
    /// 模块顺序设置['customer','clue'] (Sort Setting)
    /// </summary>
    [MaxLength(500)]
    public string? SortSetting { get; set; }

    /// <summary>
    /// 是否展示有搜索结果的列表 (Result Display)
    /// </summary>
    public bool? ResultDisplay { get; set; }

    /// <summary>
    /// 组织id (Organization ID)
    /// </summary>
    [MaxLength(50)]
    public string? OrganizationId { get; set; }
}
