using CordysCRM.CRM.Domain;
using CordysCRM.CRM.DTOs.Follow;
using CordysCRM.CRM.Repositories;

namespace CordysCRM.CRM.Services;

/// <summary>
/// 跟进计划服务实现 (Follow-Up Plan Service Implementation)
/// Converted from Java FollowUpPlanService
/// </summary>
public class FollowUpPlanService : IFollowUpPlanService
{
    private readonly IFollowUpPlanRepository _repository;

    public FollowUpPlanService(IFollowUpPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task<FollowUpPlan> AddAsync(FollowUpPlanAddRequest request, string userId, string organizationId)
    {
        var plan = new FollowUpPlan
        {
            CustomerId = request.CustomerId,
            OpportunityId = request.OpportunityId,
            Type = request.Type,
            ClueId = request.ClueId,
            Content = request.Content,
            Owner = request.Owner ?? userId,
            ContactId = request.ContactId,
            EstimatedTime = request.EstimatedTime,
            Method = request.Method,
            Status = "PREPARED",
            Converted = false,
            OrganizationId = organizationId,
            CreateUser = userId,
            CreateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            UpdateUser = userId,
            UpdateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };

        return await _repository.AddAsync(plan);
    }

    public async Task<FollowUpPlan> UpdateAsync(FollowUpPlanUpdateRequest request, string userId, string organizationId)
    {
        var plan = await _repository.GetByIdAsync(request.Id);
        if (plan == null)
        {
            throw new KeyNotFoundException($"Follow-Up Plan with ID {request.Id} not found");
        }

        if (plan.OrganizationId != organizationId)
        {
            throw new UnauthorizedAccessException("Access denied to this Follow-Up Plan");
        }

        if (!string.IsNullOrEmpty(request.Content))
            plan.Content = request.Content;
        if (!string.IsNullOrEmpty(request.Owner))
            plan.Owner = request.Owner;
        if (!string.IsNullOrEmpty(request.ContactId))
            plan.ContactId = request.ContactId;
        if (request.EstimatedTime.HasValue)
            plan.EstimatedTime = request.EstimatedTime;
        if (!string.IsNullOrEmpty(request.Method))
            plan.Method = request.Method;

        plan.UpdateUser = userId;
        plan.UpdateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        return await _repository.UpdateAsync(plan);
    }

    public async Task DeleteAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<FollowUpPlan?> GetAsync(string id, string organizationId)
    {
        var plan = await _repository.GetByIdAsync(id);
        if (plan != null && plan.OrganizationId != organizationId)
        {
            throw new UnauthorizedAccessException("Access denied to this Follow-Up Plan");
        }
        return plan;
    }

    public async Task UpdateStatusAsync(FollowUpPlanStatusRequest request, string userId)
    {
        var plan = await _repository.GetByIdAsync(request.Id);
        if (plan == null)
        {
            throw new KeyNotFoundException($"Follow-Up Plan with ID {request.Id} not found");
        }

        plan.Status = request.Status;
        plan.UpdateUser = userId;
        plan.UpdateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        await _repository.UpdateAsync(plan);
    }

    public async Task<List<FollowUpPlanListResponse>> GetListAsync(string organizationId, string? userId = null, int page = 1, int pageSize = 20)
    {
        var plans = await _repository.QueryAsync(organizationId, userId, page, pageSize);
        
        return plans.Select(p => new FollowUpPlanListResponse
        {
            Id = p.Id,
            CustomerId = p.CustomerId,
            OpportunityId = p.OpportunityId,
            Type = p.Type,
            ClueId = p.ClueId,
            Content = p.Content,
            OrganizationId = p.OrganizationId,
            Owner = p.Owner,
            ContactId = p.ContactId,
            EstimatedTime = p.EstimatedTime,
            Method = p.Method,
            Status = p.Status,
            Converted = p.Converted,
            CreateUser = p.CreateUser,
            UpdateUser = p.UpdateUser,
            CreateTime = p.CreateTime,
            UpdateTime = p.UpdateTime
        }).ToList();
    }
}
