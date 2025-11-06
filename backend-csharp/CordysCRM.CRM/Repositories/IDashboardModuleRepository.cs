using CordysCRM.CRM.Domain;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// 仪表板模块仓储接口 (Dashboard Module Repository Interface)
/// Converted from Java DashboardModuleMapper
/// </summary>
public interface IDashboardModuleRepository
{
    /// <summary>
    /// 根据ID获取仪表板模块 (Get Dashboard Module by ID)
    /// </summary>
    Task<DashboardModule?> GetByIdAsync(string id);

    /// <summary>
    /// 添加仪表板模块 (Add Dashboard Module)
    /// </summary>
    Task<DashboardModule> AddAsync(DashboardModule module);

    /// <summary>
    /// 更新仪表板模块 (Update Dashboard Module)
    /// </summary>
    Task<DashboardModule> UpdateAsync(DashboardModule module);

    /// <summary>
    /// 删除仪表板模块 (Delete Dashboard Module)
    /// </summary>
    Task DeleteAsync(string id);

    /// <summary>
    /// 根据组织ID获取所有模块 (Get All Modules by Organization ID)
    /// </summary>
    Task<List<DashboardModule>> GetByOrganizationIdAsync(string organizationId);
}
