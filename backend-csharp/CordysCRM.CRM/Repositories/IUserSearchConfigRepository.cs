using CordysCRM.CRM.Domain;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// 用户搜索配置仓储接口 (User Search Config Repository Interface)
/// Converted from Java UserSearchConfigMapper
/// </summary>
public interface IUserSearchConfigRepository
{
    /// <summary>
    /// 根据ID获取配置 (Get Config by ID)
    /// </summary>
    Task<UserSearchConfig?> GetByIdAsync(string id);

    /// <summary>
    /// 添加配置 (Add Config)
    /// </summary>
    Task<UserSearchConfig> AddAsync(UserSearchConfig config);

    /// <summary>
    /// 更新配置 (Update Config)
    /// </summary>
    Task<UserSearchConfig> UpdateAsync(UserSearchConfig config);

    /// <summary>
    /// 删除配置 (Delete Config)
    /// </summary>
    Task DeleteAsync(string id);

    /// <summary>
    /// 根据用户ID和组织ID查询配置 (Query by User ID and Organization ID)
    /// </summary>
    Task<List<UserSearchConfig>> GetByUserIdAsync(string userId, string organizationId);
}
