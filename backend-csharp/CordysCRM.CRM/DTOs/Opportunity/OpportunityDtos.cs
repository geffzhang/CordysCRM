using System.ComponentModel.DataAnnotations;

namespace CordysCRM.CRM.DTOs.Opportunity;

/// <summary>
/// 添加商机请求 (Add Opportunity Request)
/// </summary>
public record OpportunityAddRequest
{
    [Required]
    [MaxLength(200)]
    public string Name { get; init; } = string.Empty;

    [MaxLength(50)]
    public string? CustomerId { get; init; }

    public decimal? Amount { get; init; }

    public decimal? Possible { get; init; }

    [MaxLength(50)]
    public string? Stage { get; init; }

    public List<string>? Products { get; init; }

    [MaxLength(50)]
    public string? ContactId { get; init; }

    [MaxLength(50)]
    public string? Owner { get; init; }

    public long? ExpectedEndTime { get; init; }
}

/// <summary>
/// 更新商机请求 (Update Opportunity Request)
/// </summary>
public record OpportunityUpdateRequest
{
    [MaxLength(200)]
    public string? Name { get; init; }

    public decimal? Amount { get; init; }

    public decimal? Possible { get; init; }

    [MaxLength(50)]
    public string? Stage { get; init; }

    public List<string>? Products { get; init; }

    [MaxLength(50)]
    public string? ContactId { get; init; }

    public long? ExpectedEndTime { get; init; }

    [MaxLength(500)]
    public string? FailureReason { get; init; }
}

/// <summary>
/// 商机分页请求 (Opportunity Page Request)
/// </summary>
public record OpportunityPageRequest
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
    
    [MaxLength(50)]
    public string? Owner { get; init; }
    
    [MaxLength(50)]
    public string? Stage { get; init; }
    
    [MaxLength(50)]
    public string? CustomerId { get; init; }
    
    [MaxLength(100)]
    public string? SearchKeyword { get; init; }

    public decimal? MinAmount { get; init; }
    
    public decimal? MaxAmount { get; init; }
}

/// <summary>
/// 商机阶段更新请求 (Opportunity Stage Update Request)
/// </summary>
public record OpportunityStageRequest
{
    [Required]
    [MaxLength(50)]
    public string Stage { get; init; } = string.Empty;

    [MaxLength(500)]
    public string? FailureReason { get; init; }
}

/// <summary>
/// 商机转移请求 (Opportunity Transfer Request)
/// </summary>
public record OpportunityTransferRequest
{
    [Required]
    public List<string> OpportunityIds { get; init; } = new();
    
    [Required]
    [MaxLength(50)]
    public string NewOwner { get; init; } = string.Empty;
}

/// <summary>
/// 商机排序请求 (Opportunity Sort Request)
/// </summary>
public record OpportunitySortRequest
{
    [Required]
    [MaxLength(50)]
    public string OpportunityId { get; init; } = string.Empty;

    [Required]
    public long Position { get; init; }
}

/// <summary>
/// 商机响应 (Opportunity Response)
/// </summary>
public record OpportunityResponse
{
    public string Id { get; init; } = string.Empty;
    public string? CustomerId { get; init; }
    public string? Name { get; init; }
    public decimal? Amount { get; init; }
    public decimal? Possible { get; init; }
    public List<string>? Products { get; init; }
    public string? OrganizationId { get; init; }
    public string? LastStage { get; init; }
    public string? Stage { get; init; }
    public string? ContactId { get; init; }
    public string? Owner { get; init; }
    public string? Follower { get; init; }
    public long? FollowTime { get; init; }
    public long? ExpectedEndTime { get; init; }
    public long? ActualEndTime { get; init; }
    public string? FailureReason { get; init; }
    public long? Pos { get; init; }
    public long? CreateTime { get; init; }
    public long? UpdateTime { get; init; }
}

/// <summary>
/// 商机列表响应 (Opportunity List Response)
/// </summary>
public record OpportunityListResponse
{
    public List<OpportunityResponse> Items { get; init; } = new();
    public int TotalCount { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}

/// <summary>
/// 商机统计响应 (Opportunity Statistics Response)
/// </summary>
public record OpportunityStatisticsResponse
{
    public int TotalCount { get; init; }
    public decimal TotalAmount { get; init; }
    public Dictionary<string, int> StageDistribution { get; init; } = new();
    public Dictionary<string, decimal> StageAmounts { get; init; } = new();
}
