using HtmxAppServer.Logging;
using HtmxAppServer.Services;

namespace HtmxAppServer.Middleware;

public class HtmxDebuggingMiddleware(RequestDelegate next, HtmxRequestTracker requestTracker, ILogger<HtmxDebuggingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly HtmxRequestTracker _requestTracker = requestTracker;
    private readonly ILogger<HtmxDebuggingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        string requestId = context.TraceIdentifier;
        
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

            await _next(context).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.HtmxRequestError(ex, requestId, ex.Message);
            throw;
        }
        finally
        {
            // Track the request completion
            _requestTracker.CompleteRequest(requestId);
        }
    }
}
