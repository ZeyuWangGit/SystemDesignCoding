namespace Coding.RateLimiter;
using System;
using System.Collections.Concurrent;

public class FixedWindowRateLimiter(int maxRequests, int windowSizeInSeconds)
{
    private readonly ConcurrentDictionary<int, (int count, long windowStart)> _requestCounts = new();
    private readonly Lock _lock = new Lock();

    public bool RateLimit(int customerId)
    {
        long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        lock (_lock)
        {
            if (!_requestCounts.TryGetValue(customerId, out var entry))
            {
                _requestCounts[customerId] = (1, currentTime);
                return true;
            }

            var (count, windowStart) = entry;

            if (currentTime - windowStart >= windowSizeInSeconds)
            {
                // 窗口过期，重置计数
                _requestCounts[customerId] = (1, currentTime);
                return true;
            }

            if (count < maxRequests)
            {
                // 增加请求计数
                _requestCounts[customerId] = (count + 1, windowStart);
                return true;
            }

            return false; // 超过限制
        }
    }
}