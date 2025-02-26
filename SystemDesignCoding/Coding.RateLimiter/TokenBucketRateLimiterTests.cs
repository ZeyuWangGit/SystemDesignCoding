namespace Coding.RateLimiter;

public class TokenBucketRateLimiterTests
{
    [Fact]
    public void AllowsRequestsWithinLimit()
    {
        var rateLimiter = new TokenBucketRateLimiter(5, 2, 0); // 每个用户最大 5 个令牌，1 秒补充 2 个
        int customerId = 1;
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.True(rateLimiter.TryConsume(customerId));
    }

    [Fact]
    public void BlocksRequestsOverLimit()
    {
        var rateLimiter = new TokenBucketRateLimiter(2, 1, 0); // 2 个令牌，1 秒补充 1 个
        int customerId = 1;

        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.False(rateLimiter.TryConsume(customerId)); // 第 3 次请求应被拒绝
    }

    [Fact]
    public void ReplenishesTokensOverTime()
    {
        var rateLimiter = new TokenBucketRateLimiter(2, 2, 0); // 1 秒补充 2 个令牌
        int customerId = 1;

        Assert.True(rateLimiter.TryConsume(customerId)); // 消耗 1 个
        Assert.True(rateLimiter.TryConsume(customerId)); // 消耗 1 个
        Assert.False(rateLimiter.TryConsume(customerId)); // 没有令牌

        Thread.Sleep(1000); // 等待 1 秒，应该补充 2 个令牌

        Assert.True(rateLimiter.TryConsume(customerId)); // 现在应该可以再次消耗
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.False(rateLimiter.TryConsume(customerId)); // 超出限制
    }

    [Fact]
    public void DoesNotExceedMaxTokens()
    {
        var rateLimiter = new TokenBucketRateLimiter(3, 2, 0); // 最大 3 个令牌，1 秒补充 2 个
        int customerId = 1;

        Thread.Sleep(3000); // 等待 3 秒，补充 6 个令牌，但最大值是 3
        Assert.Equal(3, rateLimiter.GetCurrentTokens(customerId)); // 桶应该最多存储 3 个令牌

        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.True(rateLimiter.TryConsume(customerId));
        Assert.False(rateLimiter.TryConsume(customerId)); // 桶已空
    }

    [Fact]
    public void SupportsMultipleCustomersIndependently()
    {
        var rateLimiter = new TokenBucketRateLimiter(2, 1, 0); // 每个用户 2 个令牌，1 秒补充 1 个
        int customer1 = 1, customer2 = 2;

        Assert.True(rateLimiter.TryConsume(customer1));
        Assert.True(rateLimiter.TryConsume(customer2));

        Assert.True(rateLimiter.TryConsume(customer1));
        Assert.True(rateLimiter.TryConsume(customer2));

        Assert.False(rateLimiter.TryConsume(customer1)); // 超出限制
        Assert.False(rateLimiter.TryConsume(customer2)); // 超出限制
    }

    [Fact]
    public void ThreadSafetyTest()
    {
        var rateLimiter = new TokenBucketRateLimiter(5, 1, 0);
        int customerId = 1;
        int allowedCount = 0;
        int threadCount = 10;
        Thread[] threads = new Thread[threadCount];

        for (int i = 0; i < threadCount; i++)
        {
            threads[i] = new Thread(() =>
            {
                if (rateLimiter.TryConsume(customerId))
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
