using CordysCRM.Framework.Domain;

namespace CordysCRM.CRM.Domain;

/// <summary>
/// Role entity - represents user roles and permissions
/// </summary>
public class Role : BaseModel
{
    /// <summary>
    /// Role name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Is this an internal/built-in role
    /// </summary>
    public bool Internal { get; set; }

    /// <summary>
    /// Data scope - defines data access permissions
    /// (all_data/dept_data/dept_and_below/own_data)
    /// </summary>
    public string DataScope { get; set; } = string.Empty;

    /// <summary>
    /// Role description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Organization ID
    /// </summary>
    public string OrganizationId { get; set; } = string.Empty;
}
