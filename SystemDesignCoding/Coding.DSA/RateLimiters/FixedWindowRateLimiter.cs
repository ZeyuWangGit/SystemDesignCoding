using System.Collections.Concurrent;

namespace Coding.DSA.RateLimiters;

public class FixedWindowRateLimiter
{
    private readonly int _maxRequests;
    private readonly int _windowSizeInSeconds;
    private readonly ConcurrentDictionary<int, (int requestCount, long windowId)> _records;
    private readonly Lock _lock;
    
    public FixedWindowRateLimiter(int maxRequests, int windowSizeInSeconds)
    {
        _maxRequests = maxRequests;
        _windowSizeInSeconds = windowSizeInSeconds;
        _records = new ConcurrentDictionary<int, (int requestCount, long windowId)>();
        _lock = new Lock();
    }

    public bool TryConsume(int customerId)
    {
        var currentWindowId = DateTimeOffset.UtcNow.ToUnixTimeSeconds() / _windowSizeInSeconds;
        lock (_lock)
        {
            if (!_records.ContainsKey(customerId))
            {
                _records.TryAdd(customerId, (1, currentWindowId));
                return true;
            }
            var (requestCount, windowId) = _records[customerId];
            if (currentWindowId != windowId)
            {
                _records[customerId] = (1, currentWindowId);
                return true;
            }

            if (requestCount < _maxRequests)
            {
                _records[customerId] = (requestCount + 1, currentWindowId);
                return true;
            }

            return false;
        }
    }
}