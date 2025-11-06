using System.ComponentModel.DataAnnotations;

namespace CordysCRM.CRM.DTOs.Follow;

/// <summary>
/// 跟进记录添加请求 (Follow-Up Record Add Request)
/// Converted from Java FollowUpRecordAddRequest
/// </summary>
public class FollowUpRecordAddRequest
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
    /// 跟进内容 (Follow-Up Content)
    /// </summary>
    [MaxLength(1000)]
    public string? Content { get; set; }

    /// <summary>
    /// 跟进时间 (Follow Time)
    /// </summary>
    public long? FollowTime { get; set; }

    /// <summary>
    /// 跟进方式 (Follow Method)
    /// </summary>
    [MaxLength(32)]
    public string? FollowMethod { get; set; }

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
    /// 自定义字段 (Module Fields)
    /// </summary>
    public List<Dictionary<string, object?>>? ModuleFields { get; set; }
}
