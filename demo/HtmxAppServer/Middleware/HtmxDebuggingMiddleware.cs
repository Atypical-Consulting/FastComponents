using HtmxAppServer.Services;

namespace HtmxAppServer.Middleware;

public class HtmxDebuggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly HtmxRequestTracker _requestTracker;
    private readonly ILogger<HtmxDebuggingMiddleware> _logger;

    public HtmxDebuggingMiddleware(RequestDelegate next, HtmxRequestTracker requestTracker, ILogger<HtmxDebuggingMiddleware> logger)
    {
        _next = next;
        _requestTracker = requestTracker;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var requestId = context.TraceIdentifier;
        
        try
        {
            // Track the request start
            _requestTracker.TrackRequest(context);

            // Add debug headers to response
            if (context.Request.Headers.ContainsKey("HX-Request"))
            {
                context.Response.Headers.Append("X-HTMX-Debug-Request-Id", requestId);
                context.Response.Headers.Append("X-HTMX-Debug-Timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString());
            }

            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in HTMX request {RequestId}: {Message}", requestId, ex.Message);
            throw;
        }
        finally
        {
            // Track the request completion
            _requestTracker.CompleteRequest(requestId);
        }
    }
}