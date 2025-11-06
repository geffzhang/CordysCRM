using CordysCRM.Framework.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CordysCRM.CRM.Domain;

/// <summary>
/// 跟进记录 (Follow-Up Record Entity)
/// Converted from Java FollowUpRecord domain model
/// </summary>
[Table("follow_up_record")]
public class FollowUpRecord : BaseModel
{
    /// <summary>
    /// 客户id (Customer ID)
    /// </summary>
    [MaxLength(50)]
    public string? CustomerId { get; set; }

    /// <summary>
    /// 商机id (Opportunity ID)
    /// </summary>
    [MaxLength(50)]
    public string? OpportunityId { get; set; }

    /// <summary>
    /// 类型 (Type)
    /// </summary>
    [MaxLength(50)]
    public string? Type { get; set; }

    /// <summary>
    /// 线索id (Clue/Lead ID)
    /// </summary>
    [MaxLength(50)]
    public string? ClueId { get; set; }

    /// <summary>
    /// 跟进内容 (Follow-Up Content)
    /// </summary>
    [MaxLength(2000)]
    public string? Content { get; set; }

    /// <summary>
    /// 组织id (Organization ID)
    /// </summary>
    [MaxLength(50)]
    public string? OrganizationId { get; set; }

    /// <summary>
    /// 跟进时间 (Follow-Up Time - Unix timestamp)
    /// </summary>
    public long? FollowTime { get; set; }

    /// <summary>
    /// 跟进方式 (Follow Method)
    /// </summary>
    [MaxLength(50)]
    public string? FollowMethod { get; set; }

    /// <summary>
    /// 负责人 (Owner)
    /// </summary>
    [MaxLength(50)]
    public string? Owner { get; set; }

    /// <summary>
    /// 联系人 (Contact ID)
    /// </summary>
    [MaxLength(50)]
    public string? ContactId { get; set; }
}
