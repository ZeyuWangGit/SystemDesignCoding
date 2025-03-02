using Moq;

namespace Coding.DSA.RateLimiters;

public class SlidingWindowRateLimiterTests
{
    [Fact]
    public void SlidingWindowRateLimiter_AllowRequestWithinLimit()
    {
        var rateLimiter = new SlidingWindowRateLimiter(3, 10);
        var customerId = 1;
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.False(rateLimiter.TryConsume(customerId));
    }
    
    [Fact]
    public void SlidingWindowRateLimiter_BlockedRequestWhenLimitIsExceeded()
    {
        var rateLimiter = new SlidingWindowRateLimiter(2, 10);
        var customerId = 1;
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.False(rateLimiter.TryConsume(customerId));
    }
    [Fact]
    public void SlidingWindowRateLimiter_ResetLimitAfterWindowIsExceeded()
    {
        var rateLimiter = new SlidingWindowRateLimiter(2, 2);
        var customerId = 1;
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.True(rateLimiter.TryConsume(customerId));
        Thread.Sleep(3000);
        Assert.True(rateLimiter.TryConsume(customerId));
    }
    
    [Fact]
    public void SlidingWindowRateLimiter_CanHandleMultipleCustomers()
    {
        var rateLimiter = new SlidingWindowRateLimiter(2, 10);
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
    public void SlidingWindowRateLimiter_CanHandleMultipleThread()
    {
        var rateLimiter = new SlidingWindowRateLimiter(5, 10);
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