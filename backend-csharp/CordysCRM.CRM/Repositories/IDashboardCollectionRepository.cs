using CordysCRM.CRM.Domain;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// 仪表板收藏仓储接口 (Dashboard Collection Repository Interface)
/// </summary>
public interface IDashboardCollectionRepository
{
    /// <summary>
    /// 检查用户是否收藏了指定仪表板 (Check if user has collected dashboard)
    /// </summary>
    Task<bool> IsCollectedAsync(string dashboardId, string userId);

    /// <summary>
    /// 添加收藏 (Add Collection)
    /// </summary>
    Task<DashboardCollection> AddAsync(DashboardCollection collection);

    /// <summary>
    /// 删除收藏 (Remove Collection)
    /// </summary>
    Task DeleteAsync(string dashboardId, string userId);

    /// <summary>
    /// 根据用户ID获取收藏列表 (Get Collections by User ID)
    /// </summary>
    Task<List<DashboardCollection>> GetByUserIdAsync(string userId);
}
