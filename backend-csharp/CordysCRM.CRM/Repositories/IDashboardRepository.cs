using CordysCRM.CRM.Domain;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// 仪表板仓储接口 (Dashboard Repository Interface)
/// Converted from Java DashboardMapper
/// </summary>
public interface IDashboardRepository
{
    /// <summary>
    /// 根据ID获取仪表板 (Get Dashboard by ID)
    /// </summary>
    Task<Dashboard?> GetByIdAsync(string id);

    /// <summary>
    /// 添加仪表板 (Add Dashboard)
    /// </summary>
    Task<Dashboard> AddAsync(Dashboard dashboard);

    /// <summary>
    /// 更新仪表板 (Update Dashboard)
    /// </summary>
    Task<Dashboard> UpdateAsync(Dashboard dashboard);

    /// <summary>
    /// 删除仪表板 (Delete Dashboard)
    /// </summary>
    Task DeleteAsync(string id);

    /// <summary>
    /// 根据条件查询仪表板列表 (Query Dashboards)
    /// </summary>
    Task<List<Dashboard>> QueryAsync(string organizationId, int page = 1, int pageSize = 20);
}
