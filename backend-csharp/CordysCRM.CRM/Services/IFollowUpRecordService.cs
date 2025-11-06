using CordysCRM.CRM.Domain;
using CordysCRM.CRM.DTOs.Follow;

namespace CordysCRM.CRM.Services;

/// <summary>
/// 跟进记录服务接口 (Follow-Up Record Service Interface)
/// Converted from Java FollowUpRecordService
/// </summary>
public interface IFollowUpRecordService
{
    /// <summary>
    /// 添加跟进记录 (Add Follow-Up Record)
    /// </summary>
    Task<FollowUpRecord> AddAsync(FollowUpRecordAddRequest request, string userId, string organizationId);

    /// <summary>
    /// 更新跟进记录 (Update Follow-Up Record)
    /// </summary>
    Task<FollowUpRecord> UpdateAsync(FollowUpRecordUpdateRequest request, string userId, string organizationId);

    /// <summary>
    /// 删除跟进记录 (Delete Follow-Up Record)
    /// </summary>
    Task DeleteAsync(string id);

    /// <summary>
    /// 获取跟进记录详情 (Get Follow-Up Record Details)
    /// </summary>
    Task<FollowUpRecord?> GetAsync(string id, string organizationId);

    /// <summary>
    /// 获取跟进记录列表 (Get Follow-Up Record List)
    /// </summary>
    Task<List<FollowUpRecordListResponse>> GetListAsync(string organizationId, string? userId = null, int page = 1, int pageSize = 20);
}
