using CordysCRM.CRM.Domain;
using CordysCRM.CRM.DTOs.System;
using CordysCRM.CRM.Repositories;
using Microsoft.Extensions.Logging;

namespace CordysCRM.CRM.Services;

/// <summary>
/// Department service implementation
/// </summary>
public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repository;
    private readonly ILogger<DepartmentService> _logger;

    public DepartmentService(
        IDepartmentRepository repository,
        ILogger<DepartmentService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<DepartmentResponse?> GetByIdAsync(string id)
    {
        var department = await _repository.GetByIdAsync(id);
        return department == null ? null : MapToResponse(department);
    }

    public async Task<List<DepartmentResponse>> GetByOrganizationAsync(string organizationId)
    {
        var departments = await _repository.GetByOrganizationAsync(organizationId);
        return departments.Select(MapToResponse).ToList();
    }

    public async Task<List<DepartmentTreeResponse>> GetTreeAsync(string organizationId)
    {
        var departments = await _repository.GetTreeAsync(organizationId);
        return BuildTree(departments, null);
    }

    public async Task<DepartmentResponse> CreateAsync(DepartmentAddRequest request)
    {
        var department = new Department
        {
            Name = request.Name,
            OrganizationId = request.OrganizationId,
            ParentId = request.ParentId,
            Pos = request.Pos,
            Resource = request.Resource,
            ResourceId = request.ResourceId
        };

        await _repository.AddAsync(department);
        _logger.LogInformation("Created department {Name} in organization {OrganizationId}", 
            request.Name, request.OrganizationId);

        return MapToResponse(department);
    }

    public async Task<DepartmentResponse> UpdateAsync(string id, DepartmentUpdateRequest request)
    {
        var department = await _repository.GetByIdAsync(id);
        if (department == null)
        {
            throw new KeyNotFoundException($"Department with id {id} not found");
        }

        department.Name = request.Name;
        department.ParentId = request.ParentId;
        department.Pos = request.Pos;

        await _repository.UpdateAsync(department);
        _logger.LogInformation("Updated department {Id}", id);

        return MapToResponse(department);
    }

    public async Task DeleteAsync(string id)
    {
        var department = await _repository.GetByIdAsync(id);
        if (department == null)
        {
            throw new KeyNotFoundException($"Department with id {id} not found");
        }
        
        await _repository.DeleteAsync(department);
        _logger.LogInformation("Deleted department {Id}", id);
    }

    private static DepartmentResponse MapToResponse(Department department)
    {
        var createTime = department.CreateTime.HasValue 
            ? DateTimeOffset.FromUnixTimeMilliseconds(department.CreateTime.Value).UtcDateTime
            : DateTime.UtcNow;
        var updateTime = department.UpdateTime.HasValue 
            ? DateTimeOffset.FromUnixTimeMilliseconds(department.UpdateTime.Value).UtcDateTime
            : DateTime.UtcNow;
            
        return new DepartmentResponse(
            department.Id,
            department.Name,
            department.OrganizationId,
            department.ParentId,
            department.Pos,
            department.Resource,
            department.ResourceId,
            createTime,
            updateTime
        );
    }

    private List<DepartmentTreeResponse> BuildTree(List<Department> departments, string? parentId)
    {
        var result = new List<DepartmentTreeResponse>();
        
        var children = departments.Where(d => d.ParentId == parentId).ToList();
        
        foreach (var dept in children)
        {
            var treeNode = new DepartmentTreeResponse(
                dept.Id,
                dept.Name,
                dept.OrganizationId,
                dept.ParentId,
                dept.Pos,
                BuildTree(departments, dept.Id)
            );
            result.Add(treeNode);
        }

        return result;
    }
}
