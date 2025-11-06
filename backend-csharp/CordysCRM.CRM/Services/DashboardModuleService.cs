using CordysCRM.CRM.Domain;
using CordysCRM.CRM.DTOs.Dashboard;
using CordysCRM.CRM.Repositories;

namespace CordysCRM.CRM.Services;

/// <summary>
/// 仪表板模块服务实现 (Dashboard Module Service Implementation)
/// Converted from Java DashboardModuleService
/// </summary>
public class DashboardModuleService : IDashboardModuleService
{
    private readonly IDashboardModuleRepository _repository;

    public DashboardModuleService(IDashboardModuleRepository repository)
    {
        _repository = repository;
    }

    public async Task AddFileModuleAsync(string name, string? parentId, string organizationId, string userId)
    {
        var module = new DashboardModule
        {
            Name = name,
            ParentId = parentId,
            OrganizationId = organizationId,
            Pos = 0,
            CreateUser = userId,
            CreateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            UpdateUser = userId,
            UpdateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };

        await _repository.AddAsync(module);
    }

    public async Task RenameAsync(string id, string newName, string userId)
    {
        var module = await _repository.GetByIdAsync(id);
        if (module == null)
        {
            throw new InvalidOperationException("Dashboard Module not found");
        }

        module.Name = newName;
        module.UpdateUser = userId;
        module.UpdateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        await _repository.UpdateAsync(module);
    }

    public async Task DeleteAsync(List<string> ids, string userId, string organizationId)
    {
        foreach (var id in ids)
        {
            await _repository.DeleteAsync(id);
        }
    }

    public async Task<List<DashboardTreeNode>> GetTreeAsync(string userId, string organizationId)
    {
        var modules = await _repository.GetByOrganizationIdAsync(organizationId);
        
        // Build tree structure
        var rootNodes = modules.Where(m => string.IsNullOrEmpty(m.ParentId))
            .Select(m => new DashboardTreeNode
            {
                Id = m.Id,
                Name = m.Name,
                Type = "folder",
                Children = new List<DashboardTreeNode>()
            }).ToList();

        return rootNodes;
    }

    public async Task<Dictionary<string, long>> GetModuleCountAsync(string userId, string organizationId)
    {
        var modules = await _repository.GetByOrganizationIdAsync(organizationId);
        
        return new Dictionary<string, long>
        {
            { "total", modules.Count }
        };
    }

    public async Task MoveNodeAsync(string id, string? targetId, string userId)
    {
        var module = await _repository.GetByIdAsync(id);
        if (module == null)
        {
            throw new InvalidOperationException("Dashboard Module not found");
        }

        module.ParentId = targetId;
        module.UpdateUser = userId;
        module.UpdateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        await _repository.UpdateAsync(module);
    }
}
