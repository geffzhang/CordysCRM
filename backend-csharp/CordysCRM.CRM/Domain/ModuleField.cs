using CordysCRM.Framework.Domain;

namespace CordysCRM.CRM.Domain;

/// <summary>
/// Module field configuration - represents custom fields in CRM modules
/// </summary>
public class ModuleField : BaseModel
{
    /// <summary>
    /// Form ID this field belongs to
    /// </summary>
    public string FormId { get; set; } = string.Empty;

    /// <summary>
    /// Field name/label
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Internal key for the field
    /// </summary>
    public string InternalKey { get; set; } = string.Empty;

    /// <summary>
    /// Field type (text, number, date, select, etc.)
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Is mobile compatible
    /// </summary>
    public bool Mobile { get; set; }

    /// <summary>
    /// Sort position
    /// </summary>
    public long Pos { get; set; }
}
