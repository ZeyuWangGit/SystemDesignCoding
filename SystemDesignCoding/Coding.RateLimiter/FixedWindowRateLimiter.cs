namespace Coding.RateLimiter;

using System;
using System.Collections.Concurrent;

public class FixedWindowRateLimiter(int maxRequests, int windowSizeInSeconds)
{
    private readonly ConcurrentDictionary<int, (int count, long currentWindowId)> _requestCounts = new();
    private readonly object _lock = new Lock();

    public bool RateLimit(int customerId)
    {
        var currentWindowId = DateTimeOffset.UtcNow.ToUnixTimeSeconds() / windowSizeInSeconds;
        lock (_lock)
        {
            if (!_requestCounts.TryGetValue(customerId, out var requestCount))
            {
                _requestCounts[customerId] = (1, currentWindowId);
                return true;
            }

            var (count, windowId) = requestCount;
            if (windowId != currentWindowId)
            {
                _requestCounts[customerId] = (1, currentWindowId);
                return true;
            }

            if (count < maxRequests)
            {
                _requestCounts[customerId] = (count + 1, currentWindowId);
                return true;
            }

            return false;
        }
    }
}