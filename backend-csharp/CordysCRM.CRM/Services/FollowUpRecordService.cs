using CordysCRM.CRM.Domain;
using CordysCRM.CRM.DTOs.Follow;
using CordysCRM.CRM.Repositories;

namespace CordysCRM.CRM.Services;

/// <summary>
/// 跟进记录服务实现 (Follow-Up Record Service Implementation)
/// Converted from Java FollowUpRecordService
/// </summary>
public class FollowUpRecordService : IFollowUpRecordService
{
    private readonly IFollowUpRecordRepository _repository;

    public FollowUpRecordService(IFollowUpRecordRepository repository)
    {
        _repository = repository;
    }

    public async Task<FollowUpRecord> AddAsync(FollowUpRecordAddRequest request, string userId, string organizationId)
    {
        var record = new FollowUpRecord
        {
            CustomerId = request.CustomerId,
            OpportunityId = request.OpportunityId,
            Type = request.Type,
            ClueId = request.ClueId,
            Content = request.Content,
            FollowTime = request.FollowTime ?? DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            FollowMethod = request.FollowMethod,
            Owner = request.Owner ?? userId,
            ContactId = request.ContactId,
            OrganizationId = organizationId,
            CreateUser = userId,
            CreateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            UpdateUser = userId,
            UpdateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };

        return await _repository.AddAsync(record);
    }

    public async Task<FollowUpRecord> UpdateAsync(FollowUpRecordUpdateRequest request, string userId, string organizationId)
    {
        var record = await _repository.GetByIdAsync(request.Id);
        if (record == null)
        {
            throw new KeyNotFoundException($"Follow-Up Record with ID {request.Id} not found");
        }

        if (record.OrganizationId != organizationId)
        {
            throw new UnauthorizedAccessException("Access denied to this Follow-Up Record");
        }

        if (!string.IsNullOrEmpty(request.Content))
            record.Content = request.Content;
        if (request.FollowTime.HasValue)
            record.FollowTime = request.FollowTime;
        if (!string.IsNullOrEmpty(request.FollowMethod))
            record.FollowMethod = request.FollowMethod;
        if (!string.IsNullOrEmpty(request.Owner))
            record.Owner = request.Owner;
        if (!string.IsNullOrEmpty(request.ContactId))
            record.ContactId = request.ContactId;

        record.UpdateUser = userId;
        record.UpdateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        return await _repository.UpdateAsync(record);
    }

    public async Task DeleteAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<FollowUpRecord?> GetAsync(string id, string organizationId)
    {
        var record = await _repository.GetByIdAsync(id);
        if (record != null && record.OrganizationId != organizationId)
        {
            throw new UnauthorizedAccessException("Access denied to this Follow-Up Record");
        }
        return record;
    }

    public async Task<List<FollowUpRecordListResponse>> GetListAsync(string organizationId, string? userId = null, int page = 1, int pageSize = 20)
    {
        var records = await _repository.QueryAsync(organizationId, userId, page, pageSize);
        
        return records.Select(r => new FollowUpRecordListResponse
        {
            Id = r.Id,
            CustomerId = r.CustomerId,
            OpportunityId = r.OpportunityId,
            Type = r.Type,
            ClueId = r.ClueId,
            Content = r.Content,
            OrganizationId = r.OrganizationId,
            FollowTime = r.FollowTime,
            FollowMethod = r.FollowMethod,
            Owner = r.Owner,
            ContactId = r.ContactId,
            CreateUser = r.CreateUser,
            UpdateUser = r.UpdateUser,
            CreateTime = r.CreateTime,
            UpdateTime = r.UpdateTime
        }).ToList();
    }
}
