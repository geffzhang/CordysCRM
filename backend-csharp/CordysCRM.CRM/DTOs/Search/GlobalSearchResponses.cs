namespace CordysCRM.CRM.DTOs.Search;

/// <summary>
/// 全局搜索响应基类 (Global Search Response Base)
/// Converted from Java global search response classes
/// </summary>
public class GlobalSearchResponseBase
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
    /// 负责人 (Owner)
    /// </summary>
    public string? Owner { get; set; }

    /// <summary>
    /// 负责人名称 (Owner Name)
    /// </summary>
    public string? OwnerName { get; set; }

    /// <summary>
    /// 创建时间 (Create Time)
    /// </summary>
    public long? CreateTime { get; set; }

    /// <summary>
    /// 电话 (Phone)
    /// </summary>
    public string? Phone { get; set; }
}

/// <summary>
/// 全局商机搜索响应 (Global Opportunity Search Response)
/// </summary>
public class GlobalOpportunityResponse : GlobalSearchResponseBase
{
    /// <summary>
    /// 商机金额 (Amount)
    /// </summary>
    public decimal? Amount { get; set; }

    /// <summary>
    /// 商机阶段 (Stage)
    /// </summary>
    public string? Stage { get; set; }
}

/// <summary>
/// 全局客户搜索响应 (Global Customer Search Response)
/// </summary>
public class GlobalCustomerResponse : GlobalSearchResponseBase
{
    /// <summary>
    /// 客户类型 (Customer Type)
    /// </summary>
    public string? CustomerType { get; set; }
}

/// <summary>
/// 全局线索搜索响应 (Global Clue/Lead Search Response)
/// </summary>
public class GlobalClueResponse : GlobalSearchResponseBase
{
    /// <summary>
    /// 线索阶段 (Stage)
    /// </summary>
    public string? Stage { get; set; }
}

/// <summary>
/// 全局联系人搜索响应 (Global Contact Search Response)
/// </summary>
public class GlobalContactResponse : GlobalSearchResponseBase
{
    /// <summary>
    /// 客户名称 (Customer Name)
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// 职位 (Position)
    /// </summary>
    public string? Position { get; set; }
}
