using System.Collections.Concurrent;

namespace Coding.DSA.RateLimiters;

public class SlidingWindowRateLimiter
{
    private readonly int _maxRequests;
    private readonly int _windowSizeInSeconds;
    private readonly ConcurrentDictionary<int, Queue<long>> _records = new();
    private readonly Lock _lock = new Lock();
    
    public SlidingWindowRateLimiter(int maxRequests, int windowSizeInSeconds)
    {
        _maxRequests = maxRequests;
        _windowSizeInSeconds = windowSizeInSeconds;
    }

    public bool TryConsume(int customerId)
    {
        var currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var record = _records.GetOrAdd(customerId, id => new Queue<long>());
        
        lock (_lock)
        {
            while (record.Count > 0 && record.Peek() < currentTime - _windowSizeInSeconds)
            {
                record.Dequeue();
            }

            if (record.Count >= _maxRequests)
            {
                return false;
            }
            record.Enqueue(currentTime);
            return true;
        }
    }
}