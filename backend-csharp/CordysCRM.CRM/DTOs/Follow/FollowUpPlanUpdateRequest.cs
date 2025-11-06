using System.ComponentModel.DataAnnotations;

namespace CordysCRM.CRM.DTOs.Follow;

/// <summary>
/// 跟进计划更新请求 (Follow-Up Plan Update Request)
/// Converted from Java FollowUpPlanUpdateRequest
/// </summary>
public class FollowUpPlanUpdateRequest
{
    /// <summary>
    /// ID
    /// </summary>
    [Required]
    [MaxLength(50)]
    public required string Id { get; set; }

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
    [MaxLength(32)]
    public string? Method { get; set; }

    /// <summary>
    /// 自定义字段 (Module Fields)
    /// </summary>
    public List<Dictionary<string, object?>>? ModuleFields { get; set; }
}
