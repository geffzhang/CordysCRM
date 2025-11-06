using Microsoft.AspNetCore.Mvc;

namespace CordysCRM.App.Controllers;

/// <summary>
/// 系统版本 (System Version Controller)
/// Converted from Java Spring Boot to C# ASP.NET Core
/// </summary>
[ApiController]
[Tags("系统版本")]
public class SystemVersionController : ControllerBase
{
    private readonly ILogger<SystemVersionController> _logger;
    // private readonly ISystemService _systemService;

    public SystemVersionController(
        ILogger<SystemVersionController> logger)
        // ISystemService systemService)
    {
        _logger = logger;
        // _systemService = systemService;
    }

    /// <summary>
    /// 获取当前系统版本 (Get Current System Version)
    /// </summary>
    [HttpGet("/system/version")]
    public IActionResult GetVersion()
    {
        // TODO: Implement service call
        // var version = _systemService.GetVersion();
        _logger.LogInformation("GetVersion called");
        
        // Return placeholder version info
        var versionInfo = new VersionInfoDto
        {
            Version = "1.3.1",
            BuildTime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
            Name = "Cordys CRM",
            Description = "新一代的开源 AI CRM 系统"
        };
        
        return Ok(versionInfo);
    }
}

/// <summary>
/// Version information DTO
/// </summary>
public record VersionInfoDto
{
    public string Version { get; init; } = string.Empty;
    public string BuildTime { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}
