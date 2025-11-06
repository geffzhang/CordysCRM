using CordysCRM.CRM.Domain;
using CordysCRM.Framework.Repositories;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// Clue repository interface
/// </summary>
public interface IClueRepository : IRepository<Clue>
{
    /// <summary>
    /// Get clues by owner
    /// </summary>
    Task<IEnumerable<Clue>> GetByOwnerAsync(string ownerId, string organizationId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get clues in shared pool
    /// </summary>
    Task<IEnumerable<Clue>> GetSharedPoolCluesAsync(string organizationId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get clues by stage
    /// </summary>
    Task<IEnumerable<Clue>> GetByStageAsync(string stage, string organizationId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get clues with pagination
    /// </summary>
    Task<(IEnumerable<Clue> Items, int TotalCount)> GetPagedAsync(
        int page, 
        int pageSize, 
        string? owner, 
        string? stage, 
        bool? inSharedPool, 
        string? searchKeyword,
        string organizationId,
        CancellationToken cancellationToken = default);
}
