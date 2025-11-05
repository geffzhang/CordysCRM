using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace CordysCRM.Framework.Middleware;

/// <summary>
/// Global exception handling middleware
/// Implements AOP-style exception handling (similar to @ControllerAdvice in Spring)
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An unhandled exception occurred");

        var response = context.Response;
        response.ContentType = "application/json";

        var errorResponse = new ErrorResponse
        {
            Message = "An error occurred processing your request",
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };

        switch (exception)
        {
            case KeyNotFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse.Message = exception.Message;
                errorResponse.ErrorCode = "NOT_FOUND";
                break;

            case ArgumentException or ArgumentNullException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = exception.Message;
                errorResponse.ErrorCode = "BAD_REQUEST";
                break;

            case UnauthorizedAccessException:
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                errorResponse.Message = "Unauthorized access";
                errorResponse.ErrorCode = "UNAUTHORIZED";
                break;

            case InvalidOperationException:
                response.StatusCode = (int)HttpStatusCode.Conflict;
                errorResponse.Message = exception.Message;
                errorResponse.ErrorCode = "CONFLICT";
                break;

            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.Message = "An unexpected error occurred";
                errorResponse.ErrorCode = "INTERNAL_ERROR";
                // In production, don't expose internal error details
                if (context.RequestServices.GetService(typeof(IHostEnvironment)) is IHostEnvironment env &&
                    env.IsDevelopment())
                {
                    errorResponse.Details = exception.ToString();
                }
                break;
        }

        var result = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await response.WriteAsync(result);
    }
}

/// <summary>
/// Error response model
/// </summary>
public class ErrorResponse
{
    public string Message { get; set; } = string.Empty;
    public string? ErrorCode { get; set; }
    public long Timestamp { get; set; }
    public string? Details { get; set; }
}

/// <summary>
/// Extension method for adding ExceptionHandlingMiddleware to pipeline
/// </summary>
public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
