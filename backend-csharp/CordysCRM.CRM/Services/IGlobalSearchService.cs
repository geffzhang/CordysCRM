using CordysCRM.CRM.DTOs.Search;

namespace CordysCRM.CRM.Services;

/// <summary>
/// 全局搜索服务接口 (Global Search Service Interface)
/// Converted from Java GlobalSearchServiceFactory and related services
/// </summary>
public interface IGlobalSearchService
{
    /// <summary>
    /// 全局搜索-商机 (Global Search Opportunity)
    /// </summary>
    Task<List<GlobalOpportunityResponse>> SearchOpportunityAsync(string keyword, string organizationId, string userId, int page = 1, int pageSize = 20);

    /// <summary>
    /// 全局搜索-客户 (Global Search Customer)
    /// </summary>
    Task<List<GlobalCustomerResponse>> SearchCustomerAsync(string keyword, string organizationId, string userId, int page = 1, int pageSize = 20);

    /// <summary>
    /// 全局搜索-线索 (Global Search Clue/Lead)
    /// </summary>
    Task<List<GlobalClueResponse>> SearchClueAsync(string keyword, string organizationId, string userId, int page = 1, int pageSize = 20);

    /// <summary>
    /// 全局搜索-联系人 (Global Search Contact)
    /// </summary>
    Task<List<GlobalContactResponse>> SearchContactAsync(string keyword, string organizationId, string userId, int page = 1, int pageSize = 20);

    /// <summary>
    /// 全局搜索-模块数量统计 (Global Search Module Count)
    /// </summary>
    Task<Dictionary<string, int>> SearchModuleCountAsync(string keyword, string organizationId, string userId);
}
