using System.ComponentModel.DataAnnotations;

namespace CordysCRM.CRM.DTOs;

/// <summary>
/// Customer create DTO
/// </summary>
public record CustomerCreateDto
{
    [Required]
    [MaxLength(200)]
    public string Name { get; init; } = string.Empty;

    [MaxLength(50)]
    public string? Owner { get; init; }

    [MaxLength(50)]
    public string? PoolId { get; init; }

    public bool InSharedPool { get; init; } = false;
}

/// <summary>
/// Customer update DTO
/// </summary>
public record CustomerUpdateDto
{
    [MaxLength(200)]
    public string? Name { get; init; }

    [MaxLength(50)]
    public string? Owner { get; init; }

    [MaxLength(50)]
    public string? PoolId { get; init; }

    public bool? InSharedPool { get; init; }

    [MaxLength(50)]
    public string? Follower { get; init; }

    public long? FollowTime { get; init; }
}

/// <summary>
/// Customer response DTO
/// </summary>
public record CustomerResponseDto
{
    public string Id { get; init; } = string.Empty;
    public string? Name { get; init; }
    public string? Owner { get; init; }
    public long? CollectionTime { get; init; }
    public string? PoolId { get; init; }
    public bool? InSharedPool { get; init; }
    public string? OrganizationId { get; init; }
    public string? Follower { get; init; }
    public long? FollowTime { get; init; }
    public string? ReasonId { get; init; }
    public long? CreateTime { get; init; }
    public long? UpdateTime { get; init; }
}
