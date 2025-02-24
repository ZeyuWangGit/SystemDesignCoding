using Xunit;

namespace Coding.CodeDesign;

public class PopularityTrackerTests
{
    [Fact]
    public void TestIncreasePopularity()
    {
        var tracker = new PopularityTracker();

        tracker.IncreasePopularity(1);
        Assert.Equal(1, tracker.GetMostPopular());

        tracker.IncreasePopularity(2);
        tracker.IncreasePopularity(2);
        Assert.Equal(2, tracker.GetMostPopular()); // 2 是最热门的
    }

    [Fact]
    public void TestDecreasePopularity()
    {
        var tracker = new PopularityTracker();
        tracker.IncreasePopularity(1);
        tracker.IncreasePopularity(2);
        tracker.IncreasePopularity(2);

        tracker.DecreasePopulatiry(2);
        Assert.Equal(1, tracker.GetMostPopular()); // 1 和 2 现在一样流行，返回 1（最早出现）

        tracker.DecreasePopulatiry(1);
        Assert.Equal(2, tracker.GetMostPopular()); // 2 是唯一的
    }

    [Fact]
    public void TestMostPopularWithMultipleEntries()
    {
        var tracker = new PopularityTracker();
        tracker.IncreasePopularity(3);
        tracker.IncreasePopularity(3);
        tracker.IncreasePopularity(1);
        tracker.IncreasePopularity(1);
        tracker.IncreasePopularity(1);

        Assert.Equal(1, tracker.GetMostPopular()); // 1 最流行

        tracker.DecreasePopulatiry(1);
        Assert.Equal(3, tracker.GetMostPopular()); // 1 和 3 平手，返回最早的 3
    }

    [Fact]
    public void TestMostPopularWhenEmpty()
    {
        var tracker = new PopularityTracker();
        Assert.Equal(-1, tracker.GetMostPopular()); // 没有内容，返回 -1
    }

    [Fact]
    public void TestDecreaseBelowZero()
    {
        var tracker = new PopularityTracker();
        tracker.IncreasePopularity(1);
        tracker.DecreasePopulatiry(1);
        Assert.Equal(-1, tracker.GetMostPopular()); // 1 被删除，返回 -1

        tracker.DecreasePopulatiry(1); // 再次调用不会崩溃
        Assert.Equal(-1, tracker.GetMostPopular());
    }
}