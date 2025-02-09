using System.Collections.Concurrent;

namespace Coding.RateLimiter;

public class SlidingWindowRateLimiter(int maxRequests, int windowSizeInSeconds)
{
    private readonly ConcurrentDictionary<int, Queue<long>> _requestLogs = new();

    public bool RateLimit(int id)
    {
        var currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        var requestQueue = _requestLogs.GetOrAdd(id, new Queue<long>());

        lock (requestQueue) 
        {
            while (requestQueue.Count > 0 && requestQueue.Peek() < currentTime - windowSizeInSeconds)
            {
                requestQueue.Dequeue();
            }

            if (requestQueue.Count >= maxRequests)
            {
                return false;
            }
            
            requestQueue.Enqueue(currentTime);
            return true;
        }
    }
}