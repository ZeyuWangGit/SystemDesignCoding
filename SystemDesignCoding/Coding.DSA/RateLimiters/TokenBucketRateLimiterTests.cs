namespace Coding.DSA.RateLimiters;

public class TokenBucketRateLimiterTests
{
    [Fact]
    public void TokenBucketRateLimiter_AllowRequestWithinLimit()
    {
        var rateLimiter = new TokenBucketRateLimiter(2, 0, 1);
        var customerId = 1;
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.False(rateLimiter.TryConsume(customerId));
    }
    
    [Fact]
    public void TokenBucketRateLimiter_BlockRequestOutOfLimit()
    {
        var rateLimiter = new TokenBucketRateLimiter(2, 0, 1);
        var customerId = 1;
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.False(rateLimiter.TryConsume(customerId));
    }
    
    [Fact]
    public void TokenBucketRateLimiter_ReFillWork()
    {
        var rateLimiter = new TokenBucketRateLimiter(2, 0, 1);
        var customerId = 1;
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.False(rateLimiter.TryConsume(customerId));
        Thread.Sleep(2000);
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.False(rateLimiter.TryConsume(customerId));
    }
    
    [Fact]
    public void TokenBucketRateLimiter_CreditWorks()
    {
        var rateLimiter = new TokenBucketRateLimiter(2, 1, 1);
        var customerId = 1;
        Assert.True(rateLimiter.TryConsume(customerId));
        Thread.Sleep(2000);
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.False(rateLimiter.TryConsume(customerId));
    }
    
    [Fact]
    public void SlidingWindowRateLimiter_CanHandleMultipleThread()
    {
        var rateLimiter = new TokenBucketRateLimiter(5, 0,10);
        var customerId = 1;
        var taskCount = 10;
        var passedCount = 0;

        // await Parallel.ForEachAsync(Enumerable.Range(0, taskCount), async (_, _) =>
        // {
        //     if (rateLimiter.TryConsume(customerId))
        //     {
        //         Interlocked.Increment(ref passedCount);
        //     }
        // });

        var tasks = Enumerable.Range(0, taskCount).Select(_ => Task.Run(() =>
        {
            if (rateLimiter.TryConsume(customerId))
            {
                Interlocked.Increment(ref passedCount);
            }
        }));

        Task.WhenAll(tasks);
        
        Assert.Equal(5, passedCount);
    }
}