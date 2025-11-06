using CordysCRM.Framework.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CordysCRM.CRM.Domain;

/// <summary>
/// 商机 (Opportunity Entity)
/// Converted from Java Opportunity domain model
/// </summary>
[Table("opportunity")]
public class Opportunity : BaseModel
{
    /// <summary>
    /// 客户id (Customer ID)
    /// </summary>
    [MaxLength(50)]
    public string? CustomerId { get; set; }

    /// <summary>
    /// 商机名称 (Opportunity Name)
    /// </summary>
    [MaxLength(200)]
    public string? Name { get; set; }

    /// <summary>
    /// 金额 (Amount)
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// 可能性 (Possibility/Probability %)
    /// </summary>
    [Column(TypeName = "decimal(5,2)")]
    public decimal? Possible { get; set; }

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
    /// 上次修改前的商机阶段 (Last Stage)
    /// </summary>
    [MaxLength(50)]
    public string? LastStage { get; set; }

    /// <summary>
    /// 商机阶段 (Current Stage)
    /// </summary>
    [MaxLength(50)]
    public string? Stage { get; set; }

    /// <summary>
    /// 联系人 (Contact ID)
    /// </summary>
    [MaxLength(50)]
    public string? ContactId { get; set; }

    /// <summary>
    /// 责任人 (Owner)
    /// </summary>
    [MaxLength(50)]
    public string? Owner { get; set; }

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
    /// 预计结束时间 (Expected End Time)
    /// </summary>
    public long? ExpectedEndTime { get; set; }

    /// <summary>
    /// 实际结束时间 (Actual End Time)
    /// </summary>
    public long? ActualEndTime { get; set; }

    /// <summary>
    /// 失败原因 (Failure Reason)
    /// </summary>
    [MaxLength(500)]
    public string? FailureReason { get; set; }

    /// <summary>
    /// 自定义排序 (Custom Position/Sort Order)
    /// </summary>
    public long? Pos { get; set; }
}
