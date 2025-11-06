using CordysCRM.Framework.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CordysCRM.CRM.Domain;

/// <summary>
/// 跟进计划 (Follow-Up Plan Entity)
/// Converted from Java FollowUpPlan domain model
/// </summary>
[Table("follow_up_plan")]
public class FollowUpPlan : BaseModel
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
    /// 预计沟通内容 (Expected Content)
    /// </summary>
    [MaxLength(2000)]
    public string? Content { get; set; }

    /// <summary>
    /// 组织id (Organization ID)
    /// </summary>
    [MaxLength(50)]
    public string? OrganizationId { get; set; }

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

    /// <summary>
    /// 预计开始时间 (Estimated Time - Unix timestamp)
    /// </summary>
    public long? EstimatedTime { get; set; }

    /// <summary>
    /// 跟进方式 (Method)
    /// </summary>
    [MaxLength(50)]
    public string? Method { get; set; }

    /// <summary>
    /// 状态 (Status)
    /// </summary>
    [MaxLength(50)]
    public string? Status { get; set; }

    /// <summary>
    /// 是否转为跟进记录 (Converted to Follow-Up Record)
    /// </summary>
    public bool? Converted { get; set; }
}
