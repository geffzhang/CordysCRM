namespace CordysCRM.Framework.Storage;

/// <summary>
/// File storage service interface
/// Converted from Java FileRepository pattern
/// </summary>
public interface IFileStorageService
{
    /// <summary>
    /// Upload file
    /// </summary>
    /// <param name="fileName">File name</param>
    /// <param name="stream">File stream</param>
    /// <param name="contentType">Content type</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>File URL or identifier</returns>
    Task<string> UploadAsync(
        string fileName, 
        Stream stream, 
        string contentType, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Download file
    /// </summary>
    /// <param name="fileId">File identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>File stream</returns>
    Task<Stream> DownloadAsync(string fileId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete file
    /// </summary>
    /// <param name="fileId">File identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task DeleteAsync(string fileId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Check if file exists
    /// </summary>
    /// <param name="fileId">File identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<bool> ExistsAsync(string fileId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get file metadata
    /// </summary>
    /// <param name="fileId">File identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<FileMetadata?> GetMetadataAsync(string fileId, CancellationToken cancellationToken = default);
}

/// <summary>
/// File metadata
/// </summary>
public record FileMetadata
{
    public string FileId { get; init; } = string.Empty;
    public string FileName { get; init; } = string.Empty;
    public string ContentType { get; init; } = string.Empty;
    public long Size { get; init; }
    public DateTime CreatedAt { get; init; }
    public string? Url { get; init; }
}
