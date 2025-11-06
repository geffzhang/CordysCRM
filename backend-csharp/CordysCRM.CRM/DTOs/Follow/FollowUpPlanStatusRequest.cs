using System.ComponentModel.DataAnnotations;

namespace CordysCRM.CRM.DTOs.Follow;

/// <summary>
/// 跟进计划状态请求 (Follow-Up Plan Status Request)
/// Converted from Java FollowUpPlanStatusRequest
/// </summary>
public class FollowUpPlanStatusRequest
{
    /// <summary>
    /// ID
    /// </summary>
    [Required]
    [MaxLength(50)]
    public required string Id { get; set; }

    /// <summary>
    /// 状态 (Status)
    /// </summary>
    [Required]
    [MaxLength(50)]
    public required string Status { get; set; }
}
