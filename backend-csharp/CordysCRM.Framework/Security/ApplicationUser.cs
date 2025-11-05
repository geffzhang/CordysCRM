using Microsoft.AspNetCore.Identity;

namespace CordysCRM.Framework.Security;

/// <summary>
/// Application user entity for ASP.NET Core Identity
/// Extends IdentityUser with custom fields
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// 组织ID (Organization ID)
    /// </summary>
    public string? OrganizationId { get; set; }

    /// <summary>
    /// 真实姓名 (Real Name)
    /// </summary>
    public string? RealName { get; set; }

    /// <summary>
    /// 创建时间 (Create Time)
    /// </summary>
    public long? CreateTime { get; set; }

    /// <summary>
    /// 更新时间 (Update Time)
    /// </summary>
    public long? UpdateTime { get; set; }

    /// <summary>
    /// 是否激活 (Is Active)
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// 最后登录时间 (Last Login Time)
    /// </summary>
    public long? LastLoginTime { get; set; }
}

/// <summary>
/// Application role entity for ASP.NET Core Identity
/// </summary>
public class ApplicationRole : IdentityRole
{
    /// <summary>
    /// 描述 (Description)
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 组织ID (Organization ID)
    /// </summary>
    public string? OrganizationId { get; set; }

    /// <summary>
    /// 创建时间 (Create Time)
    /// </summary>
    public long? CreateTime { get; set; }
}
