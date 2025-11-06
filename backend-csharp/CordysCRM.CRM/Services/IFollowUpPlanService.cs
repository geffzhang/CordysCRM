using CordysCRM.CRM.Domain;
using CordysCRM.CRM.DTOs.Follow;

namespace CordysCRM.CRM.Services;

/// <summary>
/// 跟进计划服务接口 (Follow-Up Plan Service Interface)
/// Converted from Java FollowUpPlanService
/// </summary>
public interface IFollowUpPlanService
{
    /// <summary>
    /// 添加跟进计划 (Add Follow-Up Plan)
    /// </summary>
    Task<FollowUpPlan> AddAsync(FollowUpPlanAddRequest request, string userId, string organizationId);

    /// <summary>
    /// 更新跟进计划 (Update Follow-Up Plan)
    /// </summary>
    Task<FollowUpPlan> UpdateAsync(FollowUpPlanUpdateRequest request, string userId, string organizationId);

    /// <summary>
    /// 删除跟进计划 (Delete Follow-Up Plan)
    /// </summary>
    Task DeleteAsync(string id);

    /// <summary>
    /// 获取跟进计划详情 (Get Follow-Up Plan Details)
    /// </summary>
    Task<FollowUpPlan?> GetAsync(string id, string organizationId);

    /// <summary>
    /// 更新跟进计划状态 (Update Follow-Up Plan Status)
    /// </summary>
    Task UpdateStatusAsync(FollowUpPlanStatusRequest request, string userId);

    /// <summary>
    /// 获取跟进计划列表 (Get Follow-Up Plan List)
    /// </summary>
    Task<List<FollowUpPlanListResponse>> GetListAsync(string organizationId, string? userId = null, int page = 1, int pageSize = 20);
}
