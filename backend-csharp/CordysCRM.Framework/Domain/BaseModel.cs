using System.ComponentModel.DataAnnotations;

namespace CordysCRM.Framework.Domain;

/// <summary>
/// Base model for all entities
/// Converted from Java BaseModel
/// </summary>
public abstract class BaseModel
{
    /// <summary>
    /// ID (Primary Key)
    /// </summary>
    [Key]
    [MaxLength(50)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// 创建人 (Creator)
    /// </summary>
    [MaxLength(50)]
    public string? CreateUser { get; set; }

    /// <summary>
    /// 修改人 (Updater)
    /// </summary>
    [MaxLength(50)]
    public string? UpdateUser { get; set; }

    /// <summary>
    /// 创建时间 (Create Time - Unix timestamp in milliseconds)
    /// </summary>
    public long? CreateTime { get; set; }

    /// <summary>
    /// 更新时间 (Update Time - Unix timestamp in milliseconds)
    /// </summary>
    public long? UpdateTime { get; set; }
}
