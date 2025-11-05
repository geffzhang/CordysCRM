using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CordysCRM.Framework.Middleware;

/// <summary>
/// Middleware for logging HTTP requests and responses
/// Implements AOP-style cross-cutting concern (similar to AspectJ in Java)
/// </summary>
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Start timing the request
        var stopwatch = Stopwatch.StartNew();
        
        // Log request
        _logger.LogInformation(
            "HTTP {Method} {Path} started",
            context.Request.Method,
            context.Request.Path);

        try
        {
            // Call the next middleware in the pipeline
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();

            // Log response
            _logger.LogInformation(
                "HTTP {Method} {Path} responded {StatusCode} in {ElapsedMs}ms",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds);
        }
    }
}

/// <summary>
/// Extension method for adding RequestLoggingMiddleware to pipeline
/// </summary>
public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}
