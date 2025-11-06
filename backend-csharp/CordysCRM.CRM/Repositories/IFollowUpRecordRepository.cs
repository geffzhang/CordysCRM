using CordysCRM.CRM.Domain;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// 跟进记录仓储接口 (Follow-Up Record Repository Interface)
/// Converted from Java FollowUpRecordMapper
/// </summary>
public interface IFollowUpRecordRepository
{
    /// <summary>
    /// 根据ID获取跟进记录 (Get Follow-Up Record by ID)
    /// </summary>
    Task<FollowUpRecord?> GetByIdAsync(string id);

    /// <summary>
    /// 添加跟进记录 (Add Follow-Up Record)
    /// </summary>
    Task<FollowUpRecord> AddAsync(FollowUpRecord record);

    /// <summary>
    /// 更新跟进记录 (Update Follow-Up Record)
    /// </summary>
    Task<FollowUpRecord> UpdateAsync(FollowUpRecord record);

    /// <summary>
    /// 删除跟进记录 (Delete Follow-Up Record)
    /// </summary>
    Task DeleteAsync(string id);

    /// <summary>
    /// 根据条件查询跟进记录列表 (Query Follow-Up Records)
    /// </summary>
    Task<List<FollowUpRecord>> QueryAsync(string organizationId, string? userId = null, int page = 1, int pageSize = 20);
}
