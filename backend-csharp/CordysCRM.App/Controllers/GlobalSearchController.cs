using CordysCRM.CRM.Services;
using CordysCRM.Framework.Security;
using Microsoft.AspNetCore.Mvc;

namespace CordysCRM.App.Controllers;

/// <summary>
/// 全局搜索 (Global Search Controller)
/// Converted from Java GlobalSearchController
/// </summary>
[ApiController]
[Route("global/search")]
[Tags("全局搜索")]
public class GlobalSearchController : ControllerBase
{
    private readonly ILogger<GlobalSearchController> _logger;
    private readonly IGlobalSearchService _globalSearchService;
    private readonly SessionUtils _sessionUtils;

    public GlobalSearchController(
        ILogger<GlobalSearchController> logger,
        IGlobalSearchService globalSearchService,
        SessionUtils sessionUtils)
    {
        _logger = logger;
        _globalSearchService = globalSearchService;
        _sessionUtils = sessionUtils;
    }

    /// <summary>
    /// 全局搜索-商机 (Global Search Opportunity)
    /// </summary>
    [HttpPost("opportunity")]
    public async Task<IActionResult> SearchOpportunity([FromBody] SearchRequest request)
    {
        var user = _sessionUtils.GetUser();
        if (user == null)
        {
            return Unauthorized(new { message = "User not authenticated" });
        }
        
        _logger.LogInformation("Global Search Opportunity called");
        var results = await _globalSearchService.SearchOpportunityAsync(request.Keyword ?? "", user.OrganizationId, user.Id, request.Page, request.PageSize);
        return Ok(new { list = results, total = results.Count });
    }

    /// <summary>
    /// 全局搜索-客户 (Global Search Customer)
    /// </summary>
    [HttpPost("account")]
    public async Task<IActionResult> SearchCustomer([FromBody] SearchRequest request)
    {
        var user = _sessionUtils.GetUser();
        if (user == null)
        {
            return Unauthorized(new { message = "User not authenticated" });
        }
        
        _logger.LogInformation("Global Search Customer called");
        var results = await _globalSearchService.SearchCustomerAsync(request.Keyword ?? "", user.OrganizationId, user.Id, request.Page, request.PageSize);
        return Ok(new { list = results, total = results.Count });
    }

    /// <summary>
    /// 全局搜索-线索 (Global Search Clue/Lead)
    /// </summary>
    [HttpPost("lead")]
    public async Task<IActionResult> SearchClue([FromBody] SearchRequest request)
    {
        var user = _sessionUtils.GetUser();
        if (user == null)
        {
            return Unauthorized(new { message = "User not authenticated" });
        }
        
        _logger.LogInformation("Global Search Clue called");
        var results = await _globalSearchService.SearchClueAsync(request.Keyword ?? "", user.OrganizationId, user.Id, request.Page, request.PageSize);
        return Ok(new { list = results, total = results.Count });
    }

    /// <summary>
    /// 全局搜索-联系人 (Global Search Contact)
    /// </summary>
    [HttpPost("contact")]
    public async Task<IActionResult> SearchContact([FromBody] SearchRequest request)
    {
        var user = _sessionUtils.GetUser();
        if (user == null)
        {
            return Unauthorized(new { message = "User not authenticated" });
        }
        
        _logger.LogInformation("Global Search Contact called");
        var results = await _globalSearchService.SearchContactAsync(request.Keyword ?? "", user.OrganizationId, user.Id, request.Page, request.PageSize);
        return Ok(new { list = results, total = results.Count });
    }

    /// <summary>
    /// 全局搜索-模块数量统计 (Global Search Module Count)
    /// </summary>
    [HttpPost("module/count")]
    public async Task<IActionResult> SearchModuleCount([FromQuery] string keyword)
    {
        var user = _sessionUtils.GetUser();
        if (user == null)
        {
            return Unauthorized(new { message = "User not authenticated" });
        }
        
        _logger.LogInformation("Global Search Module Count called");
        var counts = await _globalSearchService.SearchModuleCountAsync(keyword, user.OrganizationId, user.Id);
        return Ok(counts);
    }
}

// Simple DTO for search requests
public record SearchRequest(string? Keyword, int Page = 1, int PageSize = 20);
