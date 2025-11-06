using CordysCRM.CRM.Domain;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// 搜索字段脱敏配置仓储接口 (Search Field Mask Config Repository Interface)
/// Converted from Java SearchFieldMaskConfigMapper
/// </summary>
public interface ISearchFieldMaskConfigRepository
{
    /// <summary>
    /// 根据ID获取配置 (Get Config by ID)
    /// </summary>
    Task<SearchFieldMaskConfig?> GetByIdAsync(string id);

    /// <summary>
    /// 添加配置 (Add Config)
    /// </summary>
    Task<SearchFieldMaskConfig> AddAsync(SearchFieldMaskConfig config);

    /// <summary>
    /// 更新配置 (Update Config)
    /// </summary>
    Task<SearchFieldMaskConfig> UpdateAsync(SearchFieldMaskConfig config);

    /// <summary>
    /// 删除配置 (Delete Config)
    /// </summary>
    Task DeleteAsync(string id);

    /// <summary>
    /// 根据组织ID查询配置 (Query by Organization ID)
    /// </summary>
    Task<List<SearchFieldMaskConfig>> GetByOrganizationIdAsync(string organizationId);
}
