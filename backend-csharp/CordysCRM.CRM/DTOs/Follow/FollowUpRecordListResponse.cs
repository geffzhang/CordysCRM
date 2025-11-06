namespace CordysCRM.CRM.DTOs.Follow;

/// <summary>
/// 跟进记录列表响应 (Follow-Up Record List Response)
/// Converted from Java FollowUpRecordListResponse
/// </summary>
public class FollowUpRecordListResponse
{
    /// <summary>
    /// ID
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// 客户id (Customer ID)
    /// </summary>
    public string? CustomerId { get; set; }

    /// <summary>
    /// 客户名称 (Customer Name)
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// 商机id (Opportunity ID)
    /// </summary>
    public string? OpportunityId { get; set; }

    /// <summary>
    /// 商机名称 (Opportunity Name)
    /// </summary>
    public string? OpportunityName { get; set; }

    /// <summary>
    /// 类型 (Type)
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// 线索id (Clue/Lead ID)
    /// </summary>
    public string? ClueId { get; set; }

    /// <summary>
    /// 线索名称 (Clue/Lead Name)
    /// </summary>
    public string? ClueName { get; set; }

    /// <summary>
    /// 跟进内容 (Follow-Up Content)
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// 组织id (Organization ID)
    /// </summary>
    public string? OrganizationId { get; set; }

    /// <summary>
    /// 跟进时间 (Follow Time)
    /// </summary>
    public long? FollowTime { get; set; }

    /// <summary>
    /// 跟进方式 (Follow Method)
    /// </summary>
    public string? FollowMethod { get; set; }

    /// <summary>
    /// 负责人 (Owner)
    /// </summary>
    public string? Owner { get; set; }

    /// <summary>
    /// 负责人名称 (Owner Name)
    /// </summary>
    public string? OwnerName { get; set; }

    /// <summary>
    /// 联系人 (Contact ID)
    /// </summary>
    public string? ContactId { get; set; }

    /// <summary>
    /// 联系人名称 (Contact Name)
    /// </summary>
    public string? ContactName { get; set; }

    /// <summary>
    /// 创建人 (Create User)
    /// </summary>
    public string? CreateUser { get; set; }

    /// <summary>
    /// 创建人名称 (Create User Name)
    /// </summary>
    public string? CreateUserName { get; set; }

    /// <summary>
    /// 更新人 (Update User)
    /// </summary>
    public string? UpdateUser { get; set; }

    /// <summary>
    /// 更新人名称 (Update User Name)
    /// </summary>
    public string? UpdateUserName { get; set; }

    /// <summary>
    /// 创建时间 (Create Time)
    /// </summary>
    public long? CreateTime { get; set; }

    /// <summary>
    /// 更新时间 (Update Time)
    /// </summary>
    public long? UpdateTime { get; set; }

    /// <summary>
    /// 归属部门 (Department ID)
    /// </summary>
    public string? DepartmentId { get; set; }

    /// <summary>
    /// 归属部门名称 (Department Name)
    /// </summary>
    public string? DepartmentName { get; set; }

    /// <summary>
    /// 电话 (Phone)
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 属于公海或者线索池 (Pool ID)
    /// </summary>
    public string? PoolId { get; set; }

    /// <summary>
    /// 资源类型 (Resource Type)
    /// </summary>
    public string? ResourceType { get; set; }

    /// <summary>
    /// 自定义字段集合 (Module Fields)
    /// </summary>
    public List<Dictionary<string, object?>>? ModuleFields { get; set; }
}
