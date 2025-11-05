using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CordysCRM.Framework.Security;

namespace CordysCRM.App.Controllers;

/// <summary>
/// 认证管理 (Authentication Controller)
/// </summary>
[ApiController]
[Route("api/auth")]
[Tags("认证管理")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ILogger<AuthController> logger)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// 用户注册 (Register new user)
    /// </summary>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Username,
            Email = request.Email,
            RealName = request.RealName,
            OrganizationId = request.OrganizationId,
            CreateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            _logger.LogInformation("User {Username} created successfully", request.Username);
            return Ok(new { message = "User created successfully", userId = user.Id });
        }

        return BadRequest(new { errors = result.Errors.Select(e => e.Description) });
    }

    /// <summary>
    /// 用户登录 (Login)
    /// </summary>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        
        if (user == null)
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }

        if (!user.IsActive)
        {
            return Unauthorized(new { message = "User account is inactive" });
        }

        var result = await _signInManager.PasswordSignInAsync(
            request.Username, 
            request.Password, 
            isPersistent: request.RememberMe, 
            lockoutOnFailure: true);

        if (result.Succeeded)
        {
            user.LastLoginTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            await _userManager.UpdateAsync(user);

            _logger.LogInformation("User {Username} logged in successfully", request.Username);
            
            return Ok(new 
            { 
                message = "Login successful",
                user = new
                {
                    id = user.Id,
                    username = user.UserName,
                    email = user.Email,
                    realName = user.RealName,
                    organizationId = user.OrganizationId
                }
            });
        }

        if (result.IsLockedOut)
        {
            _logger.LogWarning("User account {Username} locked out", request.Username);
            return Unauthorized(new { message = "Account locked due to multiple failed attempts" });
        }

        return Unauthorized(new { message = "Invalid username or password" });
    }

    /// <summary>
    /// 用户登出 (Logout)
    /// </summary>
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        _logger.LogInformation("User logged out");
        return Ok(new { message = "Logged out successfully" });
    }

    /// <summary>
    /// 获取当前用户信息 (Get current user)
    /// </summary>
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var user = await _userManager.GetUserAsync(User);
        
        if (user == null)
        {
            return Unauthorized();
        }

        var roles = await _userManager.GetRolesAsync(user);

        return Ok(new
        {
            id = user.Id,
            username = user.UserName,
            email = user.Email,
            realName = user.RealName,
            organizationId = user.OrganizationId,
            roles = roles,
            isActive = user.IsActive,
            lastLoginTime = user.LastLoginTime
        });
    }

    /// <summary>
    /// 修改密码 (Change password)
    /// </summary>
    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var user = await _userManager.GetUserAsync(User);
        
        if (user == null)
        {
            return Unauthorized();
        }

        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

        if (result.Succeeded)
        {
            _logger.LogInformation("Password changed for user {Username}", user.UserName);
            return Ok(new { message = "Password changed successfully" });
        }

        return BadRequest(new { errors = result.Errors.Select(e => e.Description) });
    }
}

/// <summary>
/// Register request model
/// </summary>
public record RegisterRequest
{
    public string Username { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string? RealName { get; init; }
    public string? OrganizationId { get; init; }
}

/// <summary>
/// Login request model
/// </summary>
public record LoginRequest
{
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public bool RememberMe { get; init; } = false;
}

/// <summary>
/// Change password request model
/// </summary>
public record ChangePasswordRequest
{
    public string CurrentPassword { get; init; } = string.Empty;
    public string NewPassword { get; init; } = string.Empty;
}
