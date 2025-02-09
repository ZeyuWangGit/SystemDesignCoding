namespace Coding.RateLimiter;

public class SlidingWindowRateLimiterTests
{
    [Fact]
    public void AllowsRequestWithinLimit()
    {
        var rateLimiter = new SlidingWindowRateLimiter(3, 10);
        var id = 1;
        
        Assert.True(rateLimiter.RateLimit(id));
        Assert.True(rateLimiter.RateLimit(id));
        Assert.True(rateLimiter.RateLimit(id));
    }
    
    [Fact]
    public void BlocksRequestsOverLimit()
    {
        var rateLimiter = new SlidingWindowRateLimiter(2, 10);
        int customerId = 1;

        Assert.True(rateLimiter.RateLimit(customerId));
        Assert.True(rateLimiter.RateLimit(customerId));
        Assert.False(rateLimiter.RateLimit(customerId)); // 第3次请求应被拒绝
    }

    [Fact]
    public void ResetsAfterWindowExpires()
    {
        var rateLimiter = new SlidingWindowRateLimiter(2, 2);
        int customerId = 1;

        Assert.True(rateLimiter.RateLimit(customerId));
        Assert.True(rateLimiter.RateLimit(customerId));
        Assert.False(rateLimiter.RateLimit(customerId)); // 超限

        Thread.Sleep(3000); // 等待3秒，窗口过期

        Assert.True(rateLimiter.RateLimit(customerId)); // 应该允许新的请求
    }

    [Fact]
    public void HandlesMultipleCustomersIndependently()
    {
        var rateLimiter = new SlidingWindowRateLimiter(2, 10);
        int customer1 = 1, customer2 = 2;

        Assert.True(rateLimiter.RateLimit(customer1));
        Assert.True(rateLimiter.RateLimit(customer2));

        Assert.True(rateLimiter.RateLimit(customer1));
        Assert.True(rateLimiter.RateLimit(customer2));

        Assert.False(rateLimiter.RateLimit(customer1));
        Assert.False(rateLimiter.RateLimit(customer2));
    }

    [Fact]
    public void ThreadSafetyTest()
    {
        var rateLimiter = new SlidingWindowRateLimiter(5, 10);
        int customerId = 1;
        int allowedCount = 0;
        int threadCount = 10;
        Thread[] threads = new Thread[threadCount];

        for (int i = 0; i < threadCount; i++)
        {
            threads[i] = new Thread(() =>
            {
                if (rateLimiter.RateLimit(customerId))
                {
                    Interlocked.Increment(ref allowedCount);
                }
            });
            threads[i].Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        Assert.Equal(5, allowedCount); // 只允许5个请求
    }
}