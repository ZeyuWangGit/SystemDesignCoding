namespace Coding.DSA.RateLimiters;

public class TokenBucket1
{
    private readonly long _maxToken;
    private readonly long _refillRatePerSecond;
    private long _currentToken;
    private long _lastRefillTimestamp;
    private Lock _lock = new Lock();

    public TokenBucket1(long maxToken, long refillRatePerSecond)
    {
        _maxToken = maxToken;
        _currentToken = maxToken;
        _refillRatePerSecond = refillRatePerSecond;
        _lastRefillTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }

    public bool TryConsume()
    {
        lock (_lock)
        {
            Refill();
            if (_currentToken > 0)
            {
                _currentToken--;
                return true;
            }

            return false;
        }
    }

    private void Refill()
    {
        var currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var elapsedTime = (currentTime - _lastRefillTimestamp) / 1000;
        if (elapsedTime > 0)
        {
            var refillToken = elapsedTime * _refillRatePerSecond;
            var allToken = _currentToken + refillToken;
            if (allToken > _maxToken)
            {
                _currentToken = _maxToken;
            }
            else
            {
                _currentToken = allToken;
            }

            _lastRefillTimestamp = currentTime;
        }
    }
}