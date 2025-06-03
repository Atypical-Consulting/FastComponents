using System.Collections.Concurrent;

namespace HtmxAppServer.Services;

public class HtmxRequestTracker
{
    private readonly ConcurrentDictionary<string, RequestInfo> _activeRequests = new();
    private readonly ILogger<HtmxRequestTracker> _logger;

    public HtmxRequestTracker(ILogger<HtmxRequestTracker> logger)
    {
        _logger = logger;
    }

    public void TrackRequest(HttpContext context)
    {
        var requestId = context.TraceIdentifier;
        var path = context.Request.Path;
        var isHtmxRequest = context.Request.Headers.ContainsKey("HX-Request");
        var htmxTarget = context.Request.Headers["HX-Target"].FirstOrDefault();
        var htmxTrigger = context.Request.Headers["HX-Trigger"].FirstOrDefault();

        var requestInfo = new RequestInfo
        {
            RequestId = requestId,
            Path = path,
            IsHtmxRequest = isHtmxRequest,
            Target = htmxTarget,
            Trigger = htmxTrigger,
            StartTime = DateTime.UtcNow,
            UserAgent = context.Request.Headers.UserAgent.ToString()
        };

        _activeRequests[requestId] = requestInfo;

        // Check for potential recursive calls
        CheckForRecursiveRequests(requestInfo);

        _logger.LogInformation("HTMX Request: {RequestId} | Path: {Path} | Target: {Target} | Trigger: {Trigger}",
            requestId, path, htmxTarget, htmxTrigger);
    }

    public void CompleteRequest(string requestId)
    {
        if (_activeRequests.TryRemove(requestId, out var requestInfo))
        {
            var duration = DateTime.UtcNow - requestInfo.StartTime;
            _logger.LogInformation("HTMX Request Completed: {RequestId} | Duration: {Duration}ms",
                requestId, duration.TotalMilliseconds);
        }
    }

    private void CheckForRecursiveRequests(RequestInfo newRequest)
    {
        if (!newRequest.IsHtmxRequest) return;

        var similarRequests = _activeRequests.Values
            .Where(r => r.IsHtmxRequest && 
                       r.Path == newRequest.Path && 
                       r.Target == newRequest.Target &&
                       r.RequestId != newRequest.RequestId &&
                       DateTime.UtcNow - r.StartTime < TimeSpan.FromSeconds(10))
            .ToList();

        if (similarRequests.Count >= 2)
        {
            _logger.LogWarning("🔄 POTENTIAL RECURSIVE REQUESTS DETECTED!");
            _logger.LogWarning("Path: {Path} | Target: {Target} | Active Count: {Count}",
                newRequest.Path, newRequest.Target, similarRequests.Count + 1);
            
            foreach (var req in similarRequests.Take(3))
            {
                _logger.LogWarning("  - Active Request: {RequestId} (Started: {StartTime})",
                    req.RequestId, req.StartTime);
            }
        }

        // Check for rapid-fire requests to same target
        var recentSimilarRequests = _activeRequests.Values
            .Where(r => r.IsHtmxRequest && 
                       r.Target == newRequest.Target &&
                       DateTime.UtcNow - r.StartTime < TimeSpan.FromSeconds(2))
            .Count();

        if (recentSimilarRequests >= 5)
        {
            _logger.LogError("⚡ RAPID-FIRE REQUESTS DETECTED for target: {Target} | Count: {Count}",
                newRequest.Target, recentSimilarRequests);
        }
    }

    public List<RequestInfo> GetActiveRequests()
    {
        return _activeRequests.Values.ToList();
    }

    public Dictionary<string, object> GetDebugInfo()
    {
        var activeRequests = _activeRequests.Values.ToList();
        
        return new Dictionary<string, object>
        {
            ["TotalActiveRequests"] = activeRequests.Count,
            ["HtmxRequests"] = activeRequests.Count(r => r.IsHtmxRequest),
            ["LongRunningRequests"] = activeRequests.Count(r => DateTime.UtcNow - r.StartTime > TimeSpan.FromSeconds(5)),
            ["RequestsByTarget"] = activeRequests
                .Where(r => r.IsHtmxRequest && !string.IsNullOrEmpty(r.Target))
                .GroupBy(r => r.Target)
                .ToDictionary(g => g.Key, g => g.Count()),
            ["RequestsByPath"] = activeRequests
                .GroupBy(r => r.Path.ToString())
                .ToDictionary(g => g.Key, g => g.Count())
        };
    }
}

public class RequestInfo
{
    public string RequestId { get; set; } = "";
    public PathString Path { get; set; }
    public bool IsHtmxRequest { get; set; }
    public string? Target { get; set; }
    public string? Trigger { get; set; }
    public DateTime StartTime { get; set; }
    public string UserAgent { get; set; } = "";
}