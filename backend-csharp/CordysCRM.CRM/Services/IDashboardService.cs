using CordysCRM.CRM.Domain;
using CordysCRM.CRM.DTOs.Dashboard;

namespace CordysCRM.CRM.Services;

/// <summary>
/// 仪表板服务接口 (Dashboard Service Interface)
/// Converted from Java DashboardService
/// </summary>
public interface IDashboardService
{
    /// <summary>
    /// 添加仪表板 (Add Dashboard)
    /// </summary>
    Task<Dashboard> AddAsync(DashboardAddRequest request, string organizationId, string userId);

    /// <summary>
    /// 更新仪表板 (Update Dashboard)
    /// </summary>
    Task UpdateAsync(DashboardUpdateRequest request, string organizationId, string userId);

    /// <summary>
    /// 删除仪表板 (Delete Dashboard)
    /// </summary>
    Task DeleteAsync(string id);

    /// <summary>
    /// 获取仪表板详情 (Get Dashboard Details)
    /// </summary>
    Task<Dashboard?> GetAsync(string id);

    /// <summary>
    /// 重命名仪表板 (Rename Dashboard)
    /// </summary>
    Task RenameAsync(DashboardRenameRequest request, string userId, string organizationId);

    /// <summary>
    /// 获取仪表板列表 (Get Dashboard List)
    /// </summary>
    Task<List<DashboardPageResponse>> GetListAsync(string organizationId, int page = 1, int pageSize = 20);

    /// <summary>
    /// 获取仪表板列表（含收藏状态） (Get Dashboard List with Collection Status)
    /// </summary>
    Task<List<DashboardPageResponse>> GetListAsync(string organizationId, string? userId, int page = 1, int pageSize = 20);

    /// <summary>
    /// 收藏仪表板 (Collect Dashboard)
    /// </summary>
    Task CollectAsync(string id, string userId);

    /// <summary>
    /// 取消收藏仪表板 (Un-collect Dashboard)
    /// </summary>
    Task UnCollectAsync(string id, string userId);
}
