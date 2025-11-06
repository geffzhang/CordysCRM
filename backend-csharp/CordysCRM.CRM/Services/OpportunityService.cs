using CordysCRM.CRM.Domain;
using CordysCRM.CRM.DTOs.Opportunity;
using CordysCRM.CRM.Repositories;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CordysCRM.CRM.Services;

/// <summary>
/// Opportunity service implementation
/// </summary>
public class OpportunityService : IOpportunityService
{
    private readonly IOpportunityRepository _opportunityRepository;
    private readonly ILogger<OpportunityService> _logger;

    public OpportunityService(
        IOpportunityRepository opportunityRepository,
        ILogger<OpportunityService> logger)
    {
        _opportunityRepository = opportunityRepository ?? throw new ArgumentNullException(nameof(opportunityRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Opportunity?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _opportunityRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<OpportunityListResponse> GetPagedAsync(
        OpportunityPageRequest request,
        string organizationId,
        CancellationToken cancellationToken = default)
    {
        var (items, totalCount) = await _opportunityRepository.GetPagedAsync(
            request.Page,
            request.PageSize,
            request.Owner,
            request.Stage,
            request.CustomerId,
            request.SearchKeyword,
            request.MinAmount,
            request.MaxAmount,
            organizationId,
            cancellationToken);

        var opportunityResponses = items.Select(o => new OpportunityResponse
        {
            Id = o.Id,
            CustomerId = o.CustomerId,
            Name = o.Name,
            Amount = o.Amount,
            Possible = o.Possible,
            Products = string.IsNullOrEmpty(o.Products) ? null : JsonSerializer.Deserialize<List<string>>(o.Products),
            OrganizationId = o.OrganizationId,
            LastStage = o.LastStage,
            Stage = o.Stage,
            ContactId = o.ContactId,
            Owner = o.Owner,
            Follower = o.Follower,
            FollowTime = o.FollowTime,
            ExpectedEndTime = o.ExpectedEndTime,
            ActualEndTime = o.ActualEndTime,
            FailureReason = o.FailureReason,
            Pos = o.Pos,
            CreateTime = o.CreateTime,
            UpdateTime = o.UpdateTime
        }).ToList();

        return new OpportunityListResponse
        {
            Items = opportunityResponses,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }

    public async Task<Opportunity> CreateAsync(
        OpportunityAddRequest request,
        string userId,
        string organizationId,
        CancellationToken cancellationToken = default)
    {
        var opportunity = new Opportunity
        {
            Id = Guid.NewGuid().ToString(),
            CustomerId = request.CustomerId,
            Name = request.Name,
            Amount = request.Amount,
            Possible = request.Possible ?? 0,
            Stage = request.Stage ?? "initial",
            Products = request.Products != null ? JsonSerializer.Serialize(request.Products) : null,
            ContactId = request.ContactId,
            Owner = request.Owner,
            OrganizationId = organizationId,
            ExpectedEndTime = request.ExpectedEndTime,
            Pos = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            CreateUser = userId,
            UpdateUser = userId,
            CreateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            UpdateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };

        _logger.LogInformation("Creating opportunity: {Name} for organization: {OrgId}", request.Name, organizationId);

        return await _opportunityRepository.AddAsync(opportunity, cancellationToken);
    }

    public async Task<Opportunity> UpdateAsync(
        string id,
        OpportunityUpdateRequest request,
        string userId,
        CancellationToken cancellationToken = default)
    {
        var opportunity = await _opportunityRepository.GetByIdAsync(id, cancellationToken);

        if (opportunity == null)
        {
            throw new KeyNotFoundException($"Opportunity with ID {id} not found");
        }

        // Update fields
        if (request.Name != null) opportunity.Name = request.Name;
        if (request.Amount.HasValue) opportunity.Amount = request.Amount;
        if (request.Possible.HasValue) opportunity.Possible = request.Possible;
        if (request.Stage != null)
        {
            opportunity.LastStage = opportunity.Stage;
            opportunity.Stage = request.Stage;

            // Set actual end time if stage is won or lost
            if (request.Stage == "won" || request.Stage == "lost")
            {
                opportunity.ActualEndTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            }
        }
        if (request.Products != null) opportunity.Products = JsonSerializer.Serialize(request.Products);
        if (request.ContactId != null) opportunity.ContactId = request.ContactId;
        if (request.ExpectedEndTime.HasValue) opportunity.ExpectedEndTime = request.ExpectedEndTime;
        if (request.FailureReason != null) opportunity.FailureReason = request.FailureReason;

        opportunity.UpdateUser = userId;
        opportunity.UpdateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        _logger.LogInformation("Updating opportunity: {Id}", id);

        await _opportunityRepository.UpdateAsync(opportunity, cancellationToken);
        return opportunity;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting opportunity: {Id}", id);
        await _opportunityRepository.DeleteByIdAsync(id, cancellationToken);
    }

    public async Task<Opportunity> UpdateStageAsync(
        string id,
        OpportunityStageRequest request,
        string userId,
        CancellationToken cancellationToken = default)
    {
        var opportunity = await _opportunityRepository.GetByIdAsync(id, cancellationToken);

        if (opportunity == null)
        {
            throw new KeyNotFoundException($"Opportunity with ID {id} not found");
        }

        opportunity.LastStage = opportunity.Stage;
        opportunity.Stage = request.Stage;

        // Set actual end time if stage is won or lost
        if (request.Stage == "won" || request.Stage == "lost")
        {
            opportunity.ActualEndTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        if (!string.IsNullOrEmpty(request.FailureReason))
        {
            opportunity.FailureReason = request.FailureReason;
        }

        opportunity.UpdateUser = userId;
        opportunity.UpdateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        _logger.LogInformation("Updating opportunity stage: {Id} to {Stage}", id, request.Stage);

        await _opportunityRepository.UpdateAsync(opportunity, cancellationToken);
        return opportunity;
    }

    public async Task TransferAsync(
        OpportunityTransferRequest request,
        string userId,
        CancellationToken cancellationToken = default)
    {
        foreach (var opportunityId in request.OpportunityIds)
        {
            var opportunity = await _opportunityRepository.GetByIdAsync(opportunityId, cancellationToken);

            if (opportunity != null)
            {
                opportunity.Owner = request.NewOwner;
                opportunity.UpdateUser = userId;
                opportunity.UpdateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                await _opportunityRepository.UpdateAsync(opportunity, cancellationToken);

                _logger.LogInformation("Transferred opportunity {Id} to owner {NewOwner}", opportunityId, request.NewOwner);
            }
        }
    }

    public async Task<Opportunity> UpdatePositionAsync(
        OpportunitySortRequest request,
        string userId,
        CancellationToken cancellationToken = default)
    {
        var opportunity = await _opportunityRepository.GetByIdAsync(request.OpportunityId, cancellationToken);

        if (opportunity == null)
        {
            throw new KeyNotFoundException($"Opportunity with ID {request.OpportunityId} not found");
        }

        opportunity.Pos = request.Position;
        opportunity.UpdateUser = userId;
        opportunity.UpdateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        await _opportunityRepository.UpdateAsync(opportunity, cancellationToken);
        return opportunity;
    }

    public async Task<IEnumerable<Opportunity>> GetByOwnerAsync(
        string ownerId,
        string organizationId,
        CancellationToken cancellationToken = default)
    {
        return await _opportunityRepository.GetByOwnerAsync(ownerId, organizationId, cancellationToken);
    }

    public async Task<IEnumerable<Opportunity>> GetByCustomerAsync(
        string customerId,
        CancellationToken cancellationToken = default)
    {
        return await _opportunityRepository.GetByCustomerAsync(customerId, cancellationToken);
    }

    public async Task<OpportunityStatisticsResponse> GetStatisticsAsync(
        string organizationId,
        string? owner = null,
        CancellationToken cancellationToken = default)
    {
        var (totalCount, totalAmount, stageDistribution, stageAmounts) =
            await _opportunityRepository.GetStatisticsAsync(organizationId, owner, cancellationToken);

        return new OpportunityStatisticsResponse
        {
            TotalCount = totalCount,
            TotalAmount = totalAmount,
            StageDistribution = stageDistribution,
            StageAmounts = stageAmounts
        };
    }
}
