using CordysCRM.CRM.Domain;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// 跟进计划仓储接口 (Follow-Up Plan Repository Interface)
/// Converted from Java FollowUpPlanMapper
/// </summary>
public interface IFollowUpPlanRepository
{
    /// <summary>
    /// 根据ID获取跟进计划 (Get Follow-Up Plan by ID)
    /// </summary>
    Task<FollowUpPlan?> GetByIdAsync(string id);

    /// <summary>
    /// 添加跟进计划 (Add Follow-Up Plan)
    /// </summary>
    Task<FollowUpPlan> AddAsync(FollowUpPlan plan);

    /// <summary>
    /// 更新跟进计划 (Update Follow-Up Plan)
    /// </summary>
    Task<FollowUpPlan> UpdateAsync(FollowUpPlan plan);

    /// <summary>
    /// 删除跟进计划 (Delete Follow-Up Plan)
    /// </summary>
    Task DeleteAsync(string id);

    /// <summary>
    /// 根据条件查询跟进计划列表 (Query Follow-Up Plans)
    /// </summary>
    Task<List<FollowUpPlan>> QueryAsync(string organizationId, string? userId = null, int page = 1, int pageSize = 20);
}
