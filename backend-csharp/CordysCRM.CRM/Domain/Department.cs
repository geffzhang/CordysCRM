using CordysCRM.Framework.Domain;

namespace CordysCRM.CRM.Domain;

/// <summary>
/// Department entity - represents organizational department structure
/// </summary>
public class Department : BaseModel
{
    /// <summary>
    /// Department name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Organization ID
    /// </summary>
    public string OrganizationId { get; set; } = string.Empty;

    /// <summary>
    /// Parent department ID (null for root departments)
    /// </summary>
    public string? ParentId { get; set; }

    /// <summary>
    /// Sort position
    /// </summary>
    public long Pos { get; set; }

    /// <summary>
    /// Resource source (e.g., DingTalk, WeCom)
    /// </summary>
    public string? Resource { get; set; }

    /// <summary>
    /// Resource ID from external system
    /// </summary>
    public string? ResourceId { get; set; }
}
