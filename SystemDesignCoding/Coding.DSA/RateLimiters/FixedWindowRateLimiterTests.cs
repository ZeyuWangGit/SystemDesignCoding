using Moq;

namespace Coding.DSA.RateLimiters;

public class FixedWindowRateLimiterTests
{
    [Fact]
    public void FixedWindowRateLimiter_AllowRequestWithinLimit()
    {
        var rateLimiter = new FixedWindowRateLimiter(3, 10);
        var customerId = 1;
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.False(rateLimiter.TryConsume(customerId));
    }
    
    [Fact]
    public void FixedWindowRateLimiter_BlockedRequestWhenLimitIsExceeded()
    {
        var rateLimiter = new FixedWindowRateLimiter(2, 10);
        var customerId = 1;
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.False(rateLimiter.TryConsume(customerId));
    }
    [Fact]
    public void FixedWindowRateLimiter_ResetLimitAfterWindowIsExceeded()
    {
        var rateLimiter = new FixedWindowRateLimiter(2, 3);
        var customerId = 1;
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.True(rateLimiter.TryConsume(customerId));
        Thread.Sleep(3000);
        Assert.True(rateLimiter.TryConsume(customerId));
    }
    
    [Fact]
    public void FixedWindowRateLimiter_CanHandleMultipleCustomers()
    {
        var rateLimiter = new FixedWindowRateLimiter(2, 10);
        var customerId1 = 1;
        var customerId2 = 2;
        Assert.True(rateLimiter.TryConsume(customerId1));
        Assert.True(rateLimiter.TryConsume(customerId2));
        
        Assert.True(rateLimiter.TryConsume(customerId1));
        Assert.True(rateLimiter.TryConsume(customerId2));
        
        Assert.False(rateLimiter.TryConsume(customerId1));
        Assert.False(rateLimiter.TryConsume(customerId2));
    }
    
    [Fact]
    public void FixedWindowRateLimiter_CanHandleMultipleThread()
    {
        var rateLimiter = new FixedWindowRateLimiter(5, 10);
        var customerId = 1;
        var taskCount = 10;
        var passedCount = 0;

        var tasks = Enumerable.Range(0, taskCount)
            .Select(_ =>
            {
                if (rateLimiter.TryConsume(customerId))
                {
                    Interlocked.Increment(ref passedCount);
                }

                return Task.CompletedTask;
            }).ToArray();
        Task.WhenAll(tasks);
        Assert.Equal(5, passedCount);
    }
}