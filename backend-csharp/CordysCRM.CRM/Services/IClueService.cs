using CordysCRM.CRM.Domain;
using CordysCRM.CRM.DTOs.Clue;

namespace CordysCRM.CRM.Services;

/// <summary>
/// Clue service interface
/// </summary>
public interface IClueService
{
    /// <summary>
    /// Get clue by ID
    /// </summary>
    Task<Clue?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get clues with pagination
    /// </summary>
    Task<ClueListResponse> GetPagedAsync(CluePageRequest request, string organizationId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create new clue
    /// </summary>
    Task<Clue> CreateAsync(ClueAddRequest request, string userId, string organizationId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update clue
    /// </summary>
    Task<Clue> UpdateAsync(string id, ClueUpdateRequest request, string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete clue
    /// </summary>
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update clue status/stage
    /// </summary>
    Task<Clue> UpdateStatusAsync(string id, ClueStatusUpdateRequest request, string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Transfer clue to another owner
    /// </summary>
    Task<Clue> TransferAsync(string id, string newOwnerId, string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Batch transfer clues
    /// </summary>
    Task BatchTransferAsync(ClueBatchTransferRequest request, string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get clues by owner
    /// </summary>
    Task<IEnumerable<Clue>> GetByOwnerAsync(string ownerId, string organizationId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get shared pool clues
    /// </summary>
    Task<IEnumerable<Clue>> GetSharedPoolCluesAsync(string organizationId, CancellationToken cancellationToken = default);
}
