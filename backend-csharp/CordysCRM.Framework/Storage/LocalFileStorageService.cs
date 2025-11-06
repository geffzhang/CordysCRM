using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CordysCRM.Framework.Storage;

/// <summary>
/// Local file system storage implementation
/// Converted from Java LocalRepository
/// </summary>
public class LocalFileStorageService : IFileStorageService
{
    private readonly string _basePath;
    private readonly ILogger<LocalFileStorageService> _logger;
    private readonly string _metadataPath;

    public LocalFileStorageService(ILogger<LocalFileStorageService> logger, string? basePath = null)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _basePath = basePath ?? Path.Combine(Path.GetTempPath(), "cordys-crm", "files");
        _metadataPath = Path.Combine(_basePath, "metadata");
        
        // Ensure directories exist
        Directory.CreateDirectory(_basePath);
        Directory.CreateDirectory(_metadataPath);
    }

    public async Task<string> UploadAsync(
        string fileName, 
        Stream stream, 
        string contentType, 
        CancellationToken cancellationToken = default)
    {
        var fileId = Guid.NewGuid().ToString();
        var filePath = GetFilePath(fileId);
        var metadataPath = GetMetadataPath(fileId);

        try
        {
            // Save file
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(fileStream, cancellationToken);
            }

            // Save metadata
            var metadata = new FileMetadata
            {
                FileId = fileId,
                FileName = fileName,
                ContentType = contentType,
                Size = new FileInfo(filePath).Length,
                CreatedAt = DateTime.UtcNow,
                Url = $"/api/files/{fileId}"
            };

            var metadataJson = JsonSerializer.Serialize(metadata);
            await File.WriteAllTextAsync(metadataPath, metadataJson, cancellationToken);

            _logger.LogInformation("File uploaded: {FileId}, Name: {FileName}", fileId, fileName);
            
            return fileId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading file: {FileName}", fileName);
            
            // Cleanup on failure
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            if (File.Exists(metadataPath))
            {
                File.Delete(metadataPath);
            }
            
            throw;
        }
    }

    public async Task<Stream> DownloadAsync(string fileId, CancellationToken cancellationToken = default)
    {
        var filePath = GetFilePath(fileId);

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File not found: {fileId}");
        }

        _logger.LogInformation("File downloaded: {FileId}", fileId);
        
        // Return a FileStream that will be disposed by the caller
        return new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
    }

    public async Task DeleteAsync(string fileId, CancellationToken cancellationToken = default)
    {
        var filePath = GetFilePath(fileId);
        var metadataPath = GetMetadataPath(fileId);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        if (File.Exists(metadataPath))
        {
            File.Delete(metadataPath);
        }

        _logger.LogInformation("File deleted: {FileId}", fileId);
    }

    public async Task<bool> ExistsAsync(string fileId, CancellationToken cancellationToken = default)
    {
        var filePath = GetFilePath(fileId);
        return File.Exists(filePath);
    }

    public async Task<FileMetadata?> GetMetadataAsync(string fileId, CancellationToken cancellationToken = default)
    {
        var metadataPath = GetMetadataPath(fileId);

        if (!File.Exists(metadataPath))
        {
            return null;
        }

        var metadataJson = await File.ReadAllTextAsync(metadataPath, cancellationToken);
        return JsonSerializer.Deserialize<FileMetadata>(metadataJson);
    }

    private string GetFilePath(string fileId)
    {
        return Path.Combine(_basePath, fileId);
    }

    private string GetMetadataPath(string fileId)
    {
        return Path.Combine(_metadataPath, $"{fileId}.json");
    }
}
