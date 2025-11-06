using CordysCRM.CRM.Domain;
using CordysCRM.CRM.DTOs.Dashboard;
using CordysCRM.CRM.Repositories;

namespace CordysCRM.CRM.Services;

/// <summary>
/// 仪表板服务实现 (Dashboard Service Implementation)
/// Converted from Java DashboardService
/// </summary>
public class DashboardService : IDashboardService
{
    private readonly IDashboardRepository _repository;
    private readonly IDashboardCollectionRepository _collectionRepository;

    public DashboardService(
        IDashboardRepository repository,
        IDashboardCollectionRepository collectionRepository)
    {
        _repository = repository;
        _collectionRepository = collectionRepository;
    }

    public async Task<Dashboard> AddAsync(DashboardAddRequest request, string organizationId, string userId)
    {
        var dashboard = new Dashboard
        {
            Name = request.Name,
            ResourceUrl = request.ResourceUrl,
            DashboardModuleId = request.DashboardModuleId,
            OrganizationId = organizationId,
            ScopeId = request.ScopeIds.FirstOrDefault(),
            Description = request.Description,
            Pos = 0,
            CreateUser = userId,
            CreateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            UpdateUser = userId,
            UpdateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };

        return await _repository.AddAsync(dashboard);
    }

    public async Task UpdateAsync(DashboardUpdateRequest request, string organizationId, string userId)
    {
        var dashboard = await _repository.GetByIdAsync(request.Id);
        if (dashboard == null)
        {
            throw new KeyNotFoundException($"Dashboard with ID {request.Id} not found");
        }

        if (!string.IsNullOrEmpty(request.Name))
            dashboard.Name = request.Name;
        if (!string.IsNullOrEmpty(request.ResourceUrl))
            dashboard.ResourceUrl = request.ResourceUrl;
        if (!string.IsNullOrEmpty(request.DashboardModuleId))
            dashboard.DashboardModuleId = request.DashboardModuleId;
        if (request.ScopeIds != null && request.ScopeIds.Count > 0)
            dashboard.ScopeId = request.ScopeIds.FirstOrDefault();
        if (!string.IsNullOrEmpty(request.Description))
            dashboard.Description = request.Description;

        dashboard.UpdateUser = userId;
        dashboard.UpdateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        await _repository.UpdateAsync(dashboard);
    }

    public async Task DeleteAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<Dashboard?> GetAsync(string id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task RenameAsync(DashboardRenameRequest request, string userId, string organizationId)
    {
        var dashboard = await _repository.GetByIdAsync(request.Id);
        if (dashboard == null)
        {
            throw new KeyNotFoundException($"Dashboard with ID {request.Id} not found");
        }

        dashboard.Name = request.NewName;
        dashboard.UpdateUser = userId;
        dashboard.UpdateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        await _repository.UpdateAsync(dashboard);
    }

    public async Task<List<DashboardPageResponse>> GetListAsync(string organizationId, int page = 1, int pageSize = 20)
    {
        return await GetListAsync(organizationId, null, page, pageSize);
    }

    public async Task<List<DashboardPageResponse>> GetListAsync(string organizationId, string? userId, int page = 1, int pageSize = 20)
    {
        var dashboards = await _repository.QueryAsync(organizationId, page, pageSize);
        
        // Get user's collections if userId is provided
        var userCollections = userId != null 
            ? await _collectionRepository.GetByUserIdAsync(userId) 
            : new List<DashboardCollection>();
        
        var collectedDashboardIds = userCollections.Select(c => c.DashboardId).ToHashSet();
        
        return dashboards.Select(d => new DashboardPageResponse
        {
            Id = d.Id,
            Name = d.Name,
            ResourceUrl = d.ResourceUrl,
            DashboardModuleId = d.DashboardModuleId,
            ScopeId = d.ScopeId,
            Description = d.Description,
            CreateUser = d.CreateUser,
            CreateTime = d.CreateTime,
            IsCollected = d.Id != null && collectedDashboardIds.Contains(d.Id)
        }).ToList();
    }

    public async Task CollectAsync(string id, string userId)
    {
        // Check if already collected
        if (await _collectionRepository.IsCollectedAsync(id, userId))
        {
            return; // Already collected, do nothing
        }

        var collection = new DashboardCollection
        {
            DashboardId = id,
            UserId = userId,
            CreateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            UpdateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };

        await _collectionRepository.AddAsync(collection);
    }

    public async Task UnCollectAsync(string id, string userId)
    {
        await _collectionRepository.DeleteAsync(id, userId);
    }
}
