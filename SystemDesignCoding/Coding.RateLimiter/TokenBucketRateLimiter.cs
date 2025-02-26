using System.Collections.Concurrent;

namespace Coding.RateLimiter;

public class TokenBucketRateLimiter(int maxTokens, int refillRatePerSecond, int maxCreditToken)
{
    // 桶的最大容量
    // 每秒补充的令牌数
    private readonly ConcurrentDictionary<int, TokenBucket> _buckets = new(); // 存储每个用户的令牌桶

    public bool TryConsume(int customerId)
    {
        var bucket = _buckets.GetOrAdd(customerId, _ => new TokenBucket(maxTokens, refillRatePerSecond, maxCreditToken));
        return bucket.TryConsume();
    }

    public long GetCurrentTokens(int customerId)
    {
        var bucket = _buckets.GetOrAdd(customerId, _ => new TokenBucket(maxTokens, refillRatePerSecond, maxCreditToken));
        return bucket.GetCurrentTokens();
    }
}

class TokenBucket
{
    private readonly long _maxTokens;
    private readonly long _maxCreditTokens;
    private readonly long _refillRatePerSecond;
    private long _currentTokens;
    private long _currentCreditTokens;
    private long _lastRefillTime;
    private readonly Lock _lock = new Lock();

    public TokenBucket(long maxTokens, long refillRatePerSecond, long maxCreditTokens)
    {
        _maxTokens = maxTokens;
        _currentTokens = maxTokens;
        _maxCreditTokens = maxTokens;
        _currentCreditTokens = maxCreditTokens;
        _refillRatePerSecond = refillRatePerSecond;
        _lastRefillTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }

    public bool TryConsume()
    {
        lock (_lock)
        {
            Refill();
            
            if (_currentTokens > 0)
            {
                _currentTokens--;
                return true; // 允许请求
            } else if (_currentCreditTokens > 0)
            {
                _currentCreditTokens--;
                return true;
            }
            return false; // 拒绝请求
        }
    }

    public long GetCurrentTokens()
    {
        lock (_lock)
        {
            Refill();
            return _currentTokens;
        }
    }

    private void Refill()
    {
        var currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var elapsedSeconds = currentTime - _lastRefillTime;
        if (elapsedSeconds > 0)
        {
            var newTokens = elapsedSeconds * _refillRatePerSecond;
            var totalTokens = _currentTokens + newTokens;
            if (newTokens > _maxTokens)
            {
                var extraToken = totalTokens - _maxTokens;
                _currentTokens = _maxTokens;
                _currentCreditTokens = Math.Max(_maxCreditTokens, _currentCreditTokens + extraToken);
            }
            else
            {
                _currentTokens = newTokens;
            }

            _lastRefillTime = currentTime;
        }
    }
}