namespace CordysCRM.CRM.DTOs.Dashboard;

/// <summary>
/// 仪表板树节点 (Dashboard Tree Node)
/// Converted from Java DashboardTreeNode
/// </summary>
public class DashboardTreeNode
{
    /// <summary>
    /// ID
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// 名称 (Name)
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 节点类型, 如:仪表板文件夹, 仪表板 (Node Type)
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// 是否收藏 (My Collect)
    /// </summary>
    public bool MyCollect { get; set; } = false;

    /// <summary>
    /// 仪表板url (Resource URL)
    /// </summary>
    public string? ResourceUrl { get; set; }

    /// <summary>
    /// 子节点 (Children)
    /// </summary>
    public List<DashboardTreeNode>? Children { get; set; }
}
