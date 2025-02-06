namespace Coding.RateLimiter;

using System.Threading;
using Xunit;

public class FixedWindowRateLimiterTests
{
    [Fact]
    public void AllowsRequestsWithinLimit()
    {
        var rateLimiter = new FixedWindowRateLimiter(3, 10);
        int customerId = 1;

        Assert.True(rateLimiter.RateLimit(customerId));
        Assert.True(rateLimiter.RateLimit(customerId));
        Assert.True(rateLimiter.RateLimit(customerId));
    }

    [Fact]
    public void BlocksRequestsOverLimit()
    {
        var rateLimiter = new FixedWindowRateLimiter(2, 10);
        int customerId = 1;

        Assert.True(rateLimiter.RateLimit(customerId));
        Assert.True(rateLimiter.RateLimit(customerId));
        Assert.False(rateLimiter.RateLimit(customerId)); // 第 3 次应该被拒绝
    }

    [Fact]
    public void ResetsAfterWindowExpires()
    {
        var rateLimiter = new FixedWindowRateLimiter(2, 2);
        int customerId = 1;

        Assert.True(rateLimiter.RateLimit(customerId));
        Assert.True(rateLimiter.RateLimit(customerId));
        Assert.False(rateLimiter.RateLimit(customerId));

        Thread.Sleep(3000); // 等待 3 秒，窗口重置

        Assert.True(rateLimiter.RateLimit(customerId)); // 应该重新允许请求
    }

    [Fact]
    public void HandlesMultipleCustomers()
    {
        var rateLimiter = new FixedWindowRateLimiter(2, 10);
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
        var rateLimiter = new FixedWindowRateLimiter(5, 10);
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

        Assert.Equal(5, allowedCount); // 只允许 5 个请求
    }
}
