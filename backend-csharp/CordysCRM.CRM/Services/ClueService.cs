using CordysCRM.CRM.Domain;
using CordysCRM.CRM.DTOs.Clue;
using CordysCRM.CRM.Repositories;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CordysCRM.CRM.Services;

/// <summary>
/// Clue service implementation
/// </summary>
public class ClueService : IClueService
{
    private readonly IClueRepository _clueRepository;
    private readonly ILogger<ClueService> _logger;

    public ClueService(
        IClueRepository clueRepository,
        ILogger<ClueService> logger)
    {
        _clueRepository = clueRepository ?? throw new ArgumentNullException(nameof(clueRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Clue?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _clueRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<ClueListResponse> GetPagedAsync(
        CluePageRequest request, 
        string organizationId, 
        CancellationToken cancellationToken = default)
    {
        var (items, totalCount) = await _clueRepository.GetPagedAsync(
            request.Page,
            request.PageSize,
            request.Owner,
            request.Stage,
            request.InSharedPool,
            request.SearchKeyword,
            organizationId,
            cancellationToken);

        var clueResponses = items.Select(c => new ClueResponse
        {
            Id = c.Id,
            Name = c.Name,
            Owner = c.Owner,
            Stage = c.Stage,
            Contact = c.Contact,
            Phone = c.Phone,
            Products = string.IsNullOrEmpty(c.Products) ? null : JsonSerializer.Deserialize<List<string>>(c.Products),
            OrganizationId = c.OrganizationId,
            CollectionTime = c.CollectionTime,
            InSharedPool = c.InSharedPool,
            TransitionType = c.TransitionType,
            TransitionId = c.TransitionId,
            Follower = c.Follower,
            FollowTime = c.FollowTime,
            CreateTime = c.CreateTime,
            UpdateTime = c.UpdateTime
        }).ToList();

        return new ClueListResponse
        {
            Items = clueResponses,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }

    public async Task<Clue> CreateAsync(
        ClueAddRequest request, 
        string userId, 
        string organizationId, 
        CancellationToken cancellationToken = default)
    {
        var clue = new Clue
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.Name,
            Contact = request.Contact,
            Phone = request.Phone,
            Stage = request.Stage ?? "new",
            Products = request.Products != null ? JsonSerializer.Serialize(request.Products) : null,
            Owner = request.Owner,
            OrganizationId = organizationId,
            CollectionTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            InSharedPool = string.IsNullOrEmpty(request.Owner),
            CreateUser = userId,
            UpdateUser = userId,
            CreateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            UpdateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };

        _logger.LogInformation("Creating clue: {Name} for organization: {OrgId}", request.Name, organizationId);
        
        return await _clueRepository.AddAsync(clue, cancellationToken);
    }

    public async Task<Clue> UpdateAsync(
        string id, 
        ClueUpdateRequest request, 
        string userId, 
        CancellationToken cancellationToken = default)
    {
        var clue = await _clueRepository.GetByIdAsync(id, cancellationToken);
        
        if (clue == null)
        {
            throw new KeyNotFoundException($"Clue with ID {id} not found");
        }

        // Update fields
        if (request.Name != null) clue.Name = request.Name;
        if (request.Contact != null) clue.Contact = request.Contact;
        if (request.Phone != null) clue.Phone = request.Phone;
        if (request.Stage != null) 
        {
            clue.LastStage = clue.Stage;
            clue.Stage = request.Stage;
        }
        if (request.Products != null) clue.Products = JsonSerializer.Serialize(request.Products);
        if (request.Follower != null) clue.Follower = request.Follower;
        if (request.FollowTime.HasValue) clue.FollowTime = request.FollowTime;

        clue.UpdateUser = userId;
        clue.UpdateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        _logger.LogInformation("Updating clue: {Id}", id);

        await _clueRepository.UpdateAsync(clue, cancellationToken);
        return clue;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting clue: {Id}", id);
        await _clueRepository.DeleteByIdAsync(id, cancellationToken);
    }

    public async Task<Clue> UpdateStatusAsync(
        string id, 
        ClueStatusUpdateRequest request, 
        string userId, 
        CancellationToken cancellationToken = default)
    {
        var clue = await _clueRepository.GetByIdAsync(id, cancellationToken);
        
        if (clue == null)
        {
            throw new KeyNotFoundException($"Clue with ID {id} not found");
        }

        clue.LastStage = clue.Stage;
        clue.Stage = request.Stage;
        clue.UpdateUser = userId;
        clue.UpdateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        await _clueRepository.UpdateAsync(clue, cancellationToken);
        return clue;
    }

    public async Task<Clue> TransferAsync(
        string id, 
        string newOwnerId, 
        string userId, 
        CancellationToken cancellationToken = default)
    {
        var clue = await _clueRepository.GetByIdAsync(id, cancellationToken);
        
        if (clue == null)
        {
            throw new KeyNotFoundException($"Clue with ID {id} not found");
        }

        clue.Owner = newOwnerId;
        clue.InSharedPool = false;
        clue.UpdateUser = userId;
        clue.UpdateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        _logger.LogInformation("Transferring clue {Id} to owner {NewOwner}", id, newOwnerId);

        await _clueRepository.UpdateAsync(clue, cancellationToken);
        return clue;
    }

    public async Task BatchTransferAsync(
        ClueBatchTransferRequest request, 
        string userId, 
        CancellationToken cancellationToken = default)
    {
        foreach (var clueId in request.ClueIds)
        {
            await TransferAsync(clueId, request.NewOwner, userId, cancellationToken);
        }
    }

    public async Task<IEnumerable<Clue>> GetByOwnerAsync(
        string ownerId, 
        string organizationId, 
        CancellationToken cancellationToken = default)
    {
        return await _clueRepository.GetByOwnerAsync(ownerId, organizationId, cancellationToken);
    }

    public async Task<IEnumerable<Clue>> GetSharedPoolCluesAsync(
        string organizationId, 
        CancellationToken cancellationToken = default)
    {
        return await _clueRepository.GetSharedPoolCluesAsync(organizationId, cancellationToken);
    }
}
