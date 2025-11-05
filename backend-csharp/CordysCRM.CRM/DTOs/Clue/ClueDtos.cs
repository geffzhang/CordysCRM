using System.ComponentModel.DataAnnotations;

namespace CordysCRM.CRM.DTOs.Clue;

/// <summary>
/// 添加线索请求 (Add Clue Request)
/// </summary>
public record ClueAddRequest
{
    [Required]
    [MaxLength(200)]
    public string Name { get; init; } = string.Empty;

    [MaxLength(100)]
    public string? Contact { get; init; }

    [MaxLength(50)]
    public string? Phone { get; init; }

    [MaxLength(50)]
    public string? Stage { get; init; }

    public List<string>? Products { get; init; }

    [MaxLength(50)]
    public string? Owner { get; init; }
}

/// <summary>
/// 更新线索请求 (Update Clue Request)
/// </summary>
public record ClueUpdateRequest
{
    [MaxLength(200)]
    public string? Name { get; init; }

    [MaxLength(100)]
    public string? Contact { get; init; }

    [MaxLength(50)]
    public string? Phone { get; init; }

    [MaxLength(50)]
    public string? Stage { get; init; }

    public List<string>? Products { get; init; }

    [MaxLength(50)]
    public string? Follower { get; init; }

    public long? FollowTime { get; init; }
}

/// <summary>
/// 线索分页请求 (Clue Page Request)
/// </summary>
public record CluePageRequest
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
    
    [MaxLength(50)]
    public string? Owner { get; init; }
    
    [MaxLength(50)]
    public string? Stage { get; init; }
    
    public bool? InSharedPool { get; init; }
    
    [MaxLength(100)]
    public string? SearchKeyword { get; init; }
}

/// <summary>
/// 线索状态更新请求 (Clue Status Update Request)
/// </summary>
public record ClueStatusUpdateRequest
{
    [Required]
    [MaxLength(50)]
    public string Stage { get; init; } = string.Empty;
}

/// <summary>
/// 线索转客户请求 (Clue Transition to Customer Request)
/// </summary>
public record ClueTransitionCustomerRequest
{
    [Required]
    [MaxLength(50)]
    public string ClueId { get; init; } = string.Empty;
    
    public bool CreateContact { get; init; } = true;
}

/// <summary>
/// 线索转商机请求 (Clue Transition to Opportunity Request)
/// </summary>
public record ClueTransitionOpportunityRequest
{
    [Required]
    [MaxLength(50)]
    public string ClueId { get; init; } = string.Empty;
    
    [Required]
    [MaxLength(200)]
    public string OpportunityName { get; init; } = string.Empty;
    
    public decimal? Amount { get; init; }
}

/// <summary>
/// 线索批量转移请求 (Clue Batch Transfer Request)
/// </summary>
public record ClueBatchTransferRequest
{
    [Required]
    public List<string> ClueIds { get; init; } = new();
    
    [Required]
    [MaxLength(50)]
    public string NewOwner { get; init; } = string.Empty;
}

/// <summary>
/// 线索响应 (Clue Response)
/// </summary>
public record ClueResponse
{
    public string Id { get; init; } = string.Empty;
    public string? Name { get; init; }
    public string? Owner { get; init; }
    public string? Stage { get; init; }
    public string? Contact { get; init; }
    public string? Phone { get; init; }
    public List<string>? Products { get; init; }
    public string? OrganizationId { get; init; }
    public long? CollectionTime { get; init; }
    public bool? InSharedPool { get; init; }
    public string? TransitionType { get; init; }
    public string? TransitionId { get; init; }
    public string? Follower { get; init; }
    public long? FollowTime { get; init; }
    public long? CreateTime { get; init; }
    public long? UpdateTime { get; init; }
}

/// <summary>
/// 线索列表响应 (Clue List Response)
/// </summary>
public record ClueListResponse
{
    public List<ClueResponse> Items { get; init; } = new();
    public int TotalCount { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}
