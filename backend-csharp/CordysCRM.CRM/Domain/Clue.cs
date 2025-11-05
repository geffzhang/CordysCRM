using CordysCRM.Framework.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CordysCRM.CRM.Domain;

/// <summary>
/// 线索 (Clue/Lead Entity)
/// Converted from Java Clue domain model
/// </summary>
[Table("clue")]
public class Clue : BaseModel
{
    /// <summary>
    /// 客户名称 (Customer Name)
    /// </summary>
    [MaxLength(200)]
    public string? Name { get; set; }

    /// <summary>
    /// 负责人 (Owner)
    /// </summary>
    [MaxLength(50)]
    public string? Owner { get; set; }

    /// <summary>
    /// 阶段 (Stage)
    /// </summary>
    [MaxLength(50)]
    public string? Stage { get; set; }

    /// <summary>
    /// 上次修改前的线索阶段 (Last Stage)
    /// </summary>
    [MaxLength(50)]
    public string? LastStage { get; set; }

    /// <summary>
    /// 联系人名称 (Contact Name)
    /// </summary>
    [MaxLength(100)]
    public string? Contact { get; set; }

    /// <summary>
    /// 联系人电话 (Contact Phone)
    /// </summary>
    [MaxLength(50)]
    public string? Phone { get; set; }

    /// <summary>
    /// 意向产品id (Product IDs - JSON array)
    /// </summary>
    [MaxLength(500)]
    public string? Products { get; set; }

    /// <summary>
    /// 组织id (Organization ID)
    /// </summary>
    [MaxLength(50)]
    public string? OrganizationId { get; set; }

    /// <summary>
    /// 领取时间 (Collection Time)
    /// </summary>
    public long? CollectionTime { get; set; }

    /// <summary>
    /// 是否在线索池 (Is in Shared Pool)
    /// </summary>
    public bool? InSharedPool { get; set; }

    /// <summary>
    /// 转移成客户或者商机 (Transition Type: customer/opportunity)
    /// </summary>
    [MaxLength(20)]
    public string? TransitionType { get; set; }

    /// <summary>
    /// 客户id或者商机id (Transition Target ID)
    /// </summary>
    [MaxLength(50)]
    public string? TransitionId { get; set; }

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
    /// 线索池ID (Pool ID)
    /// </summary>
    [MaxLength(50)]
    public string? PoolId { get; set; }

    /// <summary>
    /// 线索池原因ID (Pool Reason ID)
    /// </summary>
    [MaxLength(50)]
    public string? ReasonId { get; set; }
}
