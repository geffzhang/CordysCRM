using System.ComponentModel.DataAnnotations;

namespace CordysCRM.CRM.DTOs.Follow;

/// <summary>
/// 跟进计划添加请求 (Follow-Up Plan Add Request)
/// Converted from Java FollowUpPlanAddRequest
/// </summary>
public class FollowUpPlanAddRequest
{
    /// <summary>
    /// 客户id (Customer ID)
    /// </summary>
    [MaxLength(32)]
    public string? CustomerId { get; set; }

    /// <summary>
    /// 商机id (Opportunity ID)
    /// </summary>
    [MaxLength(32)]
    public string? OpportunityId { get; set; }

    /// <summary>
    /// 类型:CUSTOMER/CLUE (Type)
    /// </summary>
    [Required]
    [MaxLength(32)]
    public required string Type { get; set; }

    /// <summary>
    /// 线索id (Clue/Lead ID)
    /// </summary>
    [MaxLength(32)]
    public string? ClueId { get; set; }

    /// <summary>
    /// 预计沟通内容 (Expected Content)
    /// </summary>
    [MaxLength(1000)]
    public string? Content { get; set; }

    /// <summary>
    /// 负责人 (Owner)
    /// </summary>
    [MaxLength(32)]
    public string? Owner { get; set; }

    /// <summary>
    /// 联系人 (Contact ID)
    /// </summary>
    [MaxLength(32)]
    public string? ContactId { get; set; }

    /// <summary>
    /// 预计开始时间 (Estimated Time)
    /// </summary>
    public long? EstimatedTime { get; set; }

    /// <summary>
    /// 跟进方式 (Method)
    /// </summary>
    [Required]
    [MaxLength(32)]
    public required string Method { get; set; }

    /// <summary>
    /// 自定义字段 (Module Fields)
    /// </summary>
    public List<Dictionary<string, object?>>? ModuleFields { get; set; }
}
