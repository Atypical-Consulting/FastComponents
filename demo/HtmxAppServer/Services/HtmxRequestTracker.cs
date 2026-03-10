using System.Collections.Concurrent;
using HtmxAppServer.Logging;

namespace HtmxAppServer.Services;

public class HtmxRequestTracker(ILogger<HtmxRequestTracker> logger)
{
    private readonly ConcurrentDictionary<string, RequestInfo> _activeRequests = [];
    private readonly ILogger<HtmxRequestTracker> _logger = logger;

    public void TrackRequest(HttpContext context)
    {
        string requestId = context.TraceIdentifier;
        PathString path = context.Request.Path;
        bool isHtmxRequest = context.Request.Headers.ContainsKey("HX-Request");
        string? htmxTarget = context.Request.Headers["HX-Target"].FirstOrDefault();
        string? htmxTrigger = context.Request.Headers["HX-Trigger"].FirstOrDefault();

        RequestInfo requestInfo = new()
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

        _logger.HtmxRequestStarted(requestId, path.ToString(), htmxTarget, htmxTrigger);
    }

    public void CompleteRequest(string requestId)
    {
        if (!_activeRequests.TryRemove(requestId, out RequestInfo? requestInfo))
        {
            return;
        }

        TimeSpan duration = DateTime.UtcNow - requestInfo.StartTime;
        _logger.HtmxRequestCompleted(requestId, duration.TotalMilliseconds);
    }

    private void CheckForRecursiveRequests(RequestInfo newRequest)
    {
        if (!newRequest.IsHtmxRequest)
        {
            return;
        }

        List<RequestInfo> similarRequests = _activeRequests.Values
            .Where(r => r.IsHtmxRequest
                && r.Path == newRequest.Path
                && r.Target == newRequest.Target
                && r.RequestId != newRequest.RequestId
                && DateTime.UtcNow - r.StartTime < TimeSpan.FromSeconds(10))
            .ToList();

        if (similarRequests.Count >= 2)
        {
            _logger.RecursiveRequestsDetected();
            _logger.RecursiveRequestDetails(newRequest.Path.ToString(), newRequest.Target, similarRequests.Count + 1);

            foreach (RequestInfo req in similarRequests.Take(3))
            {
                _logger.ActiveRequestInfo(req.RequestId, req.StartTime);
            }
        }

        // Check for rapid-fire requests to same target
        int recentSimilarRequestsCount = _activeRequests.Values
            .Count(r => r.IsHtmxRequest
                && r.Target == newRequest.Target
                && DateTime.UtcNow - r.StartTime < TimeSpan.FromSeconds(2));

        if (recentSimilarRequestsCount < 5)
        {
            return;
        }

        _logger.RapidFireRequestsDetected(newRequest.Target, recentSimilarRequestsCount);
    }

    public List<RequestInfo> GetActiveRequests()
    {
        return [.. _activeRequests.Values];
    }

    public Dictionary<string, object> GetDebugInfo()
    {
        List<RequestInfo> activeRequests = _activeRequests.Values.ToList();
        
        return new Dictionary<string, object>
        {
            ["TotalActiveRequests"] = activeRequests.Count,
            ["HtmxRequests"] = activeRequests.Count(r => r.IsHtmxRequest),
            ["LongRunningRequests"] = activeRequests.Count(r => DateTime.UtcNow - r.StartTime > TimeSpan.FromSeconds(5)),
            ["RequestsByTarget"] = activeRequests
                .Where(r => r.IsHtmxRequest && !string.IsNullOrEmpty(r.Target))
                .GroupBy(r => r.Target!)
                .ToDictionary(g => g.Key, g => g.Count()),
            ["RequestsByPath"] = activeRequests
                .GroupBy(r => r.Path.ToString())
                .ToDictionary(g => g.Key ?? string.Empty, g => g.Count())
        };
    }
}

public class RequestInfo
{
    public string RequestId { get; set; } = string.Empty;
    public PathString Path { get; set; }
    public bool IsHtmxRequest { get; set; }
    public string? Target { get; set; }
    public string? Trigger { get; set; }
    public DateTime StartTime { get; set; }
    public string UserAgent { get; set; } = string.Empty;
}
