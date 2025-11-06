namespace CordysCRM.Framework.Common;

/// <summary>
/// Permission constants for the CRM system
/// Converted from Java to C#
/// </summary>
public static class PermissionConstants
{
    // ------ SYSTEM_ROLE ------
    public const string SystemRoleRead = "SYSTEM_ROLE:READ";
    public const string SystemRoleAdd = "SYSTEM_ROLE:ADD";
    public const string SystemRoleUpdate = "SYSTEM_ROLE:UPDATE";
    public const string SystemRoleDelete = "SYSTEM_ROLE:DELETE";
    public const string SystemRoleAddUser = "SYSTEM_ROLE:ADD_USER";
    public const string SystemRoleRemoveUser = "SYSTEM_ROLE:REMOVE_USER";

    // ------ OPERATION_LOG ------
    public const string OperationLogRead = "OPERATION_LOG:READ";

    // ------ SYSTEM_NOTICE ------
    public const string SystemNoticeRead = "SYSTEM_NOTICE:READ";
    public const string SystemNoticeAdd = "SYSTEM_NOTICE:ADD";
    public const string SystemNoticeUpdate = "SYSTEM_NOTICE:UPDATE";
    public const string SystemNoticeDelete = "SYSTEM_NOTICE:DELETE";

    // ------ SYS_ORGANIZATION ------
    public const string SysOrganizationRead = "SYS_ORGANIZATION:READ";
    public const string SysOrganizationAdd = "SYS_ORGANIZATION:ADD";
    public const string SysOrganizationUpdate = "SYS_ORGANIZATION:UPDATE";
    public const string SysOrganizationDelete = "SYS_ORGANIZATION:DELETE";
    public const string SysOrganizationImport = "SYS_ORGANIZATION:IMPORT";
    public const string SysOrganizationSync = "SYS_ORGANIZATION:SYNC";
    public const string SysOrganizationUserResetPassword = "SYS_ORGANIZATION_USER:RESET_PASSWORD";

    // ------ SYSTEM_SETTING ------
    public const string SystemSettingRead = "SYSTEM_SETTING:READ";
    public const string SystemSettingUpdate = "SYSTEM_SETTING:UPDATE";
    public const string SystemSettingAdd = "SYSTEM_SETTING:ADD";
    public const string SystemSettingDelete = "SYSTEM_SETTING:DELETE";

    // ------ MODULE_SETTING ------
    public const string ModuleSettingRead = "MODULE_SETTING:READ";
    public const string ModuleSettingUpdate = "MODULE_SETTING:UPDATE";

    // ------ CUSTOMER_MANAGEMENT ------
    public const string CustomerManagementRead = "CUSTOMER_MANAGEMENT:READ";
    public const string CustomerManagementAdd = "CUSTOMER_MANAGEMENT:ADD";
    public const string CustomerManagementUpdate = "CUSTOMER_MANAGEMENT:UPDATE";
    public const string CustomerManagementRecycle = "CUSTOMER_MANAGEMENT:RECYCLE";
    public const string CustomerManagementDelete = "CUSTOMER_MANAGEMENT:DELETE";

    // ------ DASHBOARD ------
    public const string DashboardRead = "DASHBOARD:READ";
    public const string DashboardAdd = "DASHBOARD:ADD";
    public const string DashboardEdit = "DASHBOARD:EDIT";
    public const string DashboardDelete = "DASHBOARD:DELETE";
}
