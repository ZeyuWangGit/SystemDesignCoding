using System.Collections.Concurrent;

namespace Coding.RateLimiter;

public class TokenBucketRateLimiter(int maxTokens, int refillRatePerSecond)
{
    // 桶的最大容量
    // 每秒补充的令牌数
    private readonly ConcurrentDictionary<int, TokenBucket> _buckets = new(); // 存储每个用户的令牌桶

    public bool TryConsume(int customerId)
    {
        var bucket = _buckets.GetOrAdd(customerId, _ => new TokenBucket(maxTokens, refillRatePerSecond));
        return bucket.TryConsume();
    }

    public int GetCurrentTokens(int customerId)
    {
        var bucket = _buckets.GetOrAdd(customerId, _ => new TokenBucket(maxTokens, refillRatePerSecond));
        return bucket.GetCurrentTokens();
    }
}

class TokenBucket
{
    private readonly int _maxTokens;
    private int _currentTokens;
    private readonly Lock _lock = new Lock();
    private readonly Timer _timer;

    public TokenBucket(int maxTokens, int refillRatePerSecond)
    {
        _maxTokens = maxTokens;
        _currentTokens = maxTokens;

        // 计算定时器间隔（毫秒），确保每秒补充 `refillRatePerSecond` 个令牌
        var interval = 1000 / refillRatePerSecond;

        _timer = new Timer(Refill, null, interval, interval);
    }

    public bool TryConsume()
    {
        lock (_lock)
        {
            if (_currentTokens > 0)
            {
                _currentTokens--;
                return true; // 允许请求
            }
            return false; // 拒绝请求
        }
    }
    
    public int GetCurrentTokens() => _currentTokens;

    private void Refill(object? state)
    {
        lock (_lock)
        {
            if (_currentTokens < _maxTokens)
            {
                _currentTokens++;
            }
        }
    }
}