using System.Collections.Concurrent;

namespace Coding.DSA.RateLimiters;

public class TokenBucketRateLimiter
{
    private readonly ConcurrentDictionary<int, TokenBucket> _buckets;
    private readonly long _maxTokens;
    private readonly long _maxCreditTokens;
    private readonly long _refillRatePerSecond;
    
    public TokenBucketRateLimiter(long maxTokens, long maxCreditTokens, long refillRatePerSecond)
    {
        _buckets = new ConcurrentDictionary<int, TokenBucket>();
        _maxTokens = maxTokens;
        _maxCreditTokens = maxCreditTokens;
        _refillRatePerSecond = refillRatePerSecond;
    }

    public bool TryConsume(int customerId)
    {
        var bucket = _buckets.GetOrAdd(customerId, _ => new TokenBucket(_maxTokens, _maxCreditTokens, _refillRatePerSecond));
        return bucket.TryConsume();
    }

    public long GetCurrentTokens(int customerId)
    {
        var bucket = _buckets.GetOrAdd(customerId, _ => new TokenBucket(_maxTokens, _maxCreditTokens, _refillRatePerSecond));
        return bucket.GetCurrentTokens();
    }
    
}

public class TokenBucket
{
    private readonly long _maxTokens;
    private readonly long _maxCreditTokens;
    private readonly long _refillRatePerSecond;
    private long _lastRefillTime;
    private long _currentTokens;
    private long _currentCreditTokens;
    private readonly Lock _lock = new Lock();
    
    public TokenBucket(long maxTokens, long maxCreditTokens, long refillRatePerSecond)
    {
        _maxTokens = maxTokens;
        _currentTokens = maxTokens;
        _maxCreditTokens = maxCreditTokens;
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
                return true;
            }

            if (_currentCreditTokens > 0)
            {
                _currentCreditTokens--;
                return true;
            }
            return false;
        }
    }

    public long GetCurrentTokens()
    {
        lock (_lock)
        {
            return _currentTokens;
        }
    }

    private void Refill()
    {
        var currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var elapsedTime = currentTime - _lastRefillTime;
        if (elapsedTime > 0)
        {
            var filledTokens = elapsedTime * _refillRatePerSecond;
            var allTokens = filledTokens + _currentCreditTokens;
            
            if (allTokens > _maxTokens)
            {
                _currentTokens = _maxTokens;
                _currentCreditTokens = Math.Min(_maxCreditTokens, _currentCreditTokens + allTokens);
            }
            else
            {
                _currentTokens = allTokens;
            }
            _lastRefillTime = currentTime;
        }
    }
}