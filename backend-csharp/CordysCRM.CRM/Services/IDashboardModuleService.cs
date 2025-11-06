using CordysCRM.CRM.DTOs.Dashboard;

namespace CordysCRM.CRM.Services;

/// <summary>
/// 仪表板模块服务接口 (Dashboard Module Service Interface)
/// Converted from Java DashboardModuleService
/// </summary>
public interface IDashboardModuleService
{
    /// <summary>
    /// 添加文件夹 (Add File Module)
    /// </summary>
    Task AddFileModuleAsync(string name, string? parentId, string organizationId, string userId);

    /// <summary>
    /// 重命名文件夹 (Rename Folder)
    /// </summary>
    Task RenameAsync(string id, string newName, string userId);

    /// <summary>
    /// 删除文件夹 (Delete Folder)
    /// </summary>
    Task DeleteAsync(List<string> ids, string userId, string organizationId);

    /// <summary>
    /// 获取文件树 (Get Tree)
    /// </summary>
    Task<List<DashboardTreeNode>> GetTreeAsync(string userId, string organizationId);

    /// <summary>
    /// 获取模块数量 (Get Module Count)
    /// </summary>
    Task<Dictionary<string, long>> GetModuleCountAsync(string userId, string organizationId);

    /// <summary>
    /// 移动节点 (Move Node)
    /// </summary>
    Task MoveNodeAsync(string id, string? targetId, string userId);
}
