namespace CordysCRM.CRM.DTOs.System;

// Department DTOs
public record DepartmentAddRequest(
    string Name,
    string OrganizationId,
    string? ParentId,
    long Pos = 0,
    string? Resource = null,
    string? ResourceId = null
);

public record DepartmentUpdateRequest(
    string Name,
    string? ParentId,
    long Pos
);

public record DepartmentResponse(
    string Id,
    string Name,
    string OrganizationId,
    string? ParentId,
    long Pos,
    string? Resource,
    string? ResourceId,
    DateTime CreateTime,
    DateTime UpdateTime
);

public record DepartmentTreeResponse(
    string Id,
    string Name,
    string OrganizationId,
    string? ParentId,
    long Pos,
    List<DepartmentTreeResponse> Children
);

// Role DTOs
public record RoleAddRequest(
    string Name,
    string OrganizationId,
    string DataScope,
    string? Description = null
);

public record RoleUpdateRequest(
    string Name,
    string DataScope,
    string? Description
);

public record RoleResponse(
    string Id,
    string Name,
    bool Internal,
    string DataScope,
    string? Description,
    string OrganizationId,
    DateTime CreateTime,
    DateTime UpdateTime
);

// ModuleField DTOs
public record ModuleFieldAddRequest(
    string FormId,
    string Name,
    string InternalKey,
    string Type,
    bool Mobile = false,
    long Pos = 0
);

public record ModuleFieldUpdateRequest(
    string Name,
    string Type,
    bool Mobile,
    long Pos
);

public record ModuleFieldResponse(
    string Id,
    string FormId,
    string Name,
    string InternalKey,
    string Type,
    bool Mobile,
    long Pos,
    DateTime CreateTime,
    DateTime UpdateTime
);

public record ModuleFieldSortRequest(
    string Id,
    long Pos
);
