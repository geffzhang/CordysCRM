namespace CordysCRM.CRM.Constants;

/// <summary>
/// 搜索模块枚举 (Search Module Enum)
/// Converted from Java SearchModuleEnum
/// </summary>
public static class SearchModuleEnum
{
    /// <summary>
    /// 线索 (Clue/Lead)
    /// </summary>
    public const string SEARCH_ADVANCED_CLUE = "searchAdvancedClue";

    /// <summary>
    /// 客户 (Customer)
    /// </summary>
    public const string SEARCH_ADVANCED_CUSTOMER = "searchAdvancedCustomer";

    /// <summary>
    /// 联系人 (Contact)
    /// </summary>
    public const string SEARCH_ADVANCED_CONTACT = "searchAdvancedContact";

    /// <summary>
    /// 公海 (Public/Customer Pool)
    /// </summary>
    public const string SEARCH_ADVANCED_PUBLIC = "searchAdvancedPublic";

    /// <summary>
    /// 线索池 (Clue Pool)
    /// </summary>
    public const string SEARCH_ADVANCED_CLUE_POOL = "searchAdvancedCluePool";

    /// <summary>
    /// 商机 (Opportunity)
    /// </summary>
    public const string SEARCH_ADVANCED_OPPORTUNITY = "searchAdvancedOpportunity";

    /// <summary>
    /// 所有值 (All Values)
    /// </summary>
    public static readonly List<string> VALUES = new()
    {
        SEARCH_ADVANCED_CLUE,
        SEARCH_ADVANCED_CLUE_POOL,
        SEARCH_ADVANCED_CUSTOMER,
        SEARCH_ADVANCED_PUBLIC,
        SEARCH_ADVANCED_OPPORTUNITY,
        SEARCH_ADVANCED_CONTACT
    };
}
