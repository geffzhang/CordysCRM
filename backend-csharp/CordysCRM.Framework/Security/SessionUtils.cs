using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace CordysCRM.Framework.Security;

/// <summary>
/// Session 工具类，提供操作用户 Session 的常用方法。
/// Session utility class for user session operations.
/// Converted from Java to C#
/// </summary>
public class SessionUtils
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<SessionUtils> _logger;

    public SessionUtils(
        IHttpContextAccessor httpContextAccessor,
        ILogger<SessionUtils> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    /// <summary>
    /// 获取当前用户的 ID (Get current user ID)
    /// </summary>
    /// <returns>当前用户的 ID，如果没有获取到用户信息，则返回 null</returns>
    public string? GetUserId()
    {
        var user = GetUser();
        return user?.Id;
    }

    /// <summary>
    /// 获取当前用户信息 (Get current user info)
    /// </summary>
    /// <returns>当前用户对象，如果未获取到用户信息，则返回 null</returns>
    public SessionUser? GetUser()
    {
        try
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null)
            {
                return null;
            }

            if (!session.TryGetValue(SessionConstants.AttrUser, out byte[]? userBytes) || userBytes == null)
            {
                return null;
            }

            var userJson = Encoding.UTF8.GetString(userBytes);
            return JsonSerializer.Deserialize<SessionUser>(userJson);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "后台获取在线用户失败: {Message}", ex.Message);
            return null;
        }
    }

    /// <summary>
    /// 获取当前 Session 的 ID (Get current session ID)
    /// </summary>
    /// <returns>当前 Session 的 ID</returns>
    public string? GetSessionId()
    {
        return _httpContextAccessor.HttpContext?.Session.Id;
    }

    /// <summary>
    /// 将当前用户信息保存到 Session 中 (Save user info to session)
    /// </summary>
    /// <param name="sessionUser">当前用户对象</param>
    public void PutUser(SessionUser sessionUser)
    {
        var session = _httpContextAccessor.HttpContext?.Session;
        if (session == null)
        {
            throw new InvalidOperationException("Session is not available");
        }

        var userJson = JsonSerializer.Serialize(sessionUser);
        var userBytes = Encoding.UTF8.GetBytes(userJson);
        session.Set(SessionConstants.AttrUser, userBytes);
        
        var idBytes = Encoding.UTF8.GetBytes(sessionUser.Id);
        session.Set(SessionConstants.PrincipalNameIndexName, idBytes);
    }

    /// <summary>
    /// 清除当前用户的 Session (Clear current user's session)
    /// </summary>
    public void ClearSession()
    {
        var session = _httpContextAccessor.HttpContext?.Session;
        session?.Clear();
    }
}

/// <summary>
/// Session 常量 (Session constants)
/// </summary>
public static class SessionConstants
{
    public const string AttrUser = "ATTR_USER";
    public const string PrincipalNameIndexName = "PRINCIPAL_NAME_INDEX_NAME";
}

/// <summary>
/// 会话用户信息 (Session user info)
/// </summary>
public class SessionUser
{
    public string Id { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string OrganizationId { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();
    public List<string> Permissions { get; set; } = new();
}
