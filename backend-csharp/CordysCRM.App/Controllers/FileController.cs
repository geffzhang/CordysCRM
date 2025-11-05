using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CordysCRM.Framework.Storage;

namespace CordysCRM.App.Controllers;

/// <summary>
/// 文件管理 (File Management Controller)
/// </summary>
[ApiController]
[Route("api/files")]
[Tags("文件管理")]
public class FileController : ControllerBase
{
    private readonly IFileStorageService _fileStorageService;
    private readonly ILogger<FileController> _logger;

    public FileController(
        IFileStorageService fileStorageService,
        ILogger<FileController> logger)
    {
        _fileStorageService = fileStorageService ?? throw new ArgumentNullException(nameof(fileStorageService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// 上传文件 (Upload file)
    /// </summary>
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(new { message = "No file uploaded" });
        }

        // Validate file size (e.g., max 100MB)
        const long maxFileSize = 100 * 1024 * 1024; // 100MB
        if (file.Length > maxFileSize)
        {
            return BadRequest(new { message = "File size exceeds maximum limit of 100MB" });
        }

        try
        {
            using var stream = file.OpenReadStream();
            var fileId = await _fileStorageService.UploadAsync(
                file.FileName, 
                stream, 
                file.ContentType, 
                cancellationToken);

            _logger.LogInformation("File uploaded successfully: {FileId}", fileId);

            return Ok(new 
            { 
                fileId,
                fileName = file.FileName,
                size = file.Length,
                contentType = file.ContentType,
                url = $"/api/files/{fileId}"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading file: {FileName}", file.FileName);
            return StatusCode(500, new { message = "Error uploading file" });
        }
    }

    /// <summary>
    /// 下载文件 (Download file)
    /// </summary>
    [HttpGet("{fileId}")]
    public async Task<IActionResult> Download(string fileId, CancellationToken cancellationToken)
    {
        try
        {
            var metadata = await _fileStorageService.GetMetadataAsync(fileId, cancellationToken);
            
            if (metadata == null)
            {
                return NotFound(new { message = $"File not found: {fileId}" });
            }

            var stream = await _fileStorageService.DownloadAsync(fileId, cancellationToken);

            return File(stream, metadata.ContentType, metadata.FileName);
        }
        catch (FileNotFoundException)
        {
            return NotFound(new { message = $"File not found: {fileId}" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error downloading file: {FileId}", fileId);
            return StatusCode(500, new { message = "Error downloading file" });
        }
    }

    /// <summary>
    /// 获取文件元数据 (Get file metadata)
    /// </summary>
    [HttpGet("{fileId}/metadata")]
    public async Task<IActionResult> GetMetadata(string fileId, CancellationToken cancellationToken)
    {
        var metadata = await _fileStorageService.GetMetadataAsync(fileId, cancellationToken);
        
        if (metadata == null)
        {
            return NotFound(new { message = $"File not found: {fileId}" });
        }

        return Ok(metadata);
    }

    /// <summary>
    /// 删除文件 (Delete file)
    /// </summary>
    [HttpDelete("{fileId}")]
    [Authorize]
    public async Task<IActionResult> Delete(string fileId, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _fileStorageService.ExistsAsync(fileId, cancellationToken);
            
            if (!exists)
            {
                return NotFound(new { message = $"File not found: {fileId}" });
            }

            await _fileStorageService.DeleteAsync(fileId, cancellationToken);
            
            _logger.LogInformation("File deleted: {FileId}", fileId);
            
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting file: {FileId}", fileId);
            return StatusCode(500, new { message = "Error deleting file" });
        }
    }
}
