using CordysCRM.Framework.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CordysCRM.CRM.Domain;

/// <summary>
/// 客户 (Customer Entity)
/// Converted from Java Customer domain model
/// </summary>
[Table("customer")]
public class Customer : BaseModel
{
    /// <summary>
    /// 客户名称 (Customer Name)
    /// </summary>
    [MaxLength(200)]
    public string? Name { get; set; }

    /// <summary>
    /// 负责人 (Owner/Responsible Person)
    /// </summary>
    [MaxLength(50)]
    public string? Owner { get; set; }

    /// <summary>
    /// 领取时间 (Collection Time)
    /// </summary>
    public long? CollectionTime { get; set; }

    /// <summary>
    /// 公海ID (Shared Pool ID)
    /// </summary>
    [MaxLength(50)]
    public string? PoolId { get; set; }

    /// <summary>
    /// 是否在公海池 (Is in Shared Pool)
    /// </summary>
    public bool? InSharedPool { get; set; }

    /// <summary>
    /// 组织id (Organization ID)
    /// </summary>
    [MaxLength(50)]
    public string? OrganizationId { get; set; }

    /// <summary>
    /// 最新跟进人 (Latest Follower)
    /// </summary>
    [MaxLength(50)]
    public string? Follower { get; set; }

    /// <summary>
    /// 最新跟进时间 (Latest Follow Time)
    /// </summary>
    public long? FollowTime { get; set; }

    /// <summary>
    /// 公海原因ID (Pool Reason ID)
    /// </summary>
    [MaxLength(50)]
    public string? ReasonId { get; set; }
}
