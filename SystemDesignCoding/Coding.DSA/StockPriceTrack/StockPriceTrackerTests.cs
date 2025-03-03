
namespace Coding.DSA.StockPriceTrack;

public class StockPriceTrackerTests
{
    private readonly StockPriceTracker _tracker = new();

    [Fact]
    public void Test_EmptyTracker_ReturnsNegativeOne()
    {
        Assert.Equal(-1, _tracker.GetLatestPrice());
        Assert.Equal(-1, _tracker.GetMaxPrice());
        Assert.Equal(-1, _tracker.GetMinPrice());
    }

    [Fact]
    public void Test_AddOrUpdate_SingleEntry()
    {
        _tracker.AddOrUpdate("2024-02-01 10:00:00", 100);

        Assert.Equal(100, _tracker.GetLatestPrice());
        Assert.Equal(100, _tracker.GetMaxPrice());
        Assert.Equal(100, _tracker.GetMinPrice());
        Assert.Equal(100, _tracker.GetMaximumUpTo("2024-02-01 10:00:00"));
    }

    [Fact]
    public void Test_AddOrUpdate_MultipleEntries()
    {
        _tracker.AddOrUpdate("2024-02-01 10:00:00", 100);
        _tracker.AddOrUpdate("2024-02-01 11:00:00", 200);
        _tracker.AddOrUpdate("2024-02-01 12:00:00", 150);
        _tracker.AddOrUpdate("2024-02-01 13:00:00", 300);

        Assert.Equal(300, _tracker.GetLatestPrice());
        Assert.Equal(300, _tracker.GetMaxPrice());
        Assert.Equal(100, _tracker.GetMinPrice());

        Assert.Equal(200, _tracker.GetMaximumUpTo("2024-02-01 12:00:00"));
        Assert.Equal(200, _tracker.GetMaximumUpTo("2024-02-01 11:59:59")); // 没有精确匹配，取 ≤ timestamp 的最大值
        Assert.Equal(300, _tracker.GetMaximumUpTo("2024-02-01 13:00:00"));
        Assert.Equal(-1, _tracker.GetMaximumUpTo("2024-01-31 23:59:59")); // 没有匹配项，返回 -1
    }

    [Fact]
    public void Test_AddOrUpdate_UpdatingExistingTimestamp()
    {
        _tracker.AddOrUpdate("2024-02-01 10:00:00", 100);
        _tracker.AddOrUpdate("2024-02-01 11:00:00", 200);
        _tracker.AddOrUpdate("2024-02-01 11:00:00", 250); // 更新 11:00:00 的价格

        Assert.Equal(250, _tracker.GetMaximumUpTo("2024-02-01 11:00:00")); // 确保更新后最大值正确
        Assert.Equal(250, _tracker.GetMaxPrice()); // 确保最高价格更新
    }

    [Fact]
    public void Test_GetMaximumUpTo_WithDifferentTimestamps()
    {
        _tracker.AddOrUpdate("2024-02-01 10:00:00", 100);
        _tracker.AddOrUpdate("2024-02-01 11:00:00", 200);
        _tracker.AddOrUpdate("2024-02-01 12:00:00", 150);
        _tracker.AddOrUpdate("2024-02-01 13:00:00", 300);

        Assert.Equal(100, _tracker.GetMaximumUpTo("2024-02-01 10:30:00")); // 没有 10:30，取 ≤ 的最大值
        Assert.Equal(200, _tracker.GetMaximumUpTo("2024-02-01 11:30:00"));
        Assert.Equal(300, _tracker.GetMaximumUpTo("2024-02-01 13:00:00"));
        Assert.Equal(-1, _tracker.GetMaximumUpTo("2024-01-30 23:59:59")); // 时间太早，没有数据
    }

    [Fact]
    public void Test_GetMaxPrice_And_GetMinPrice()
    {
        _tracker.AddOrUpdate("2024-02-01 10:00:00", 500);
        _tracker.AddOrUpdate("2024-02-01 11:00:00", 200);
        _tracker.AddOrUpdate("2024-02-01 12:00:00", 150);
        _tracker.AddOrUpdate("2024-02-01 13:00:00", 800);

        Assert.Equal(800, _tracker.GetMaxPrice());
        Assert.Equal(150, _tracker.GetMinPrice());
    }

    [Fact]
    public void Test_GetMaximumUpTo_WhenAllPricesSame()
    {
        _tracker.AddOrUpdate("2024-02-01 10:00:00", 100);
        _tracker.AddOrUpdate("2024-02-01 11:00:00", 100);
        _tracker.AddOrUpdate("2024-02-01 12:00:00", 100);
        _tracker.AddOrUpdate("2024-02-01 13:00:00", 100);

        Assert.Equal(100, _tracker.GetMaximumUpTo("2024-02-01 12:30:00"));
        Assert.Equal(100, _tracker.GetMaxPrice());
        Assert.Equal(100, _tracker.GetMinPrice());
    }
}