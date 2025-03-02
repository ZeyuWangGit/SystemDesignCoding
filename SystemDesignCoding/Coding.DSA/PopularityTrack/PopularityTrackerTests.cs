namespace Coding.DSA.PopularityTrack;

public class PopularityTrackerTests
{
    [Fact]
    public void PopularityTracker_CanIncreasePopularity()
    {
        var tracker = new PopularityTracker();
        tracker.IncreasePopularity(1);
        Assert.Equal(1, tracker.GetMostPopularContent());
        tracker.IncreasePopularity(2);
        tracker.IncreasePopularity(2);
        Assert.Equal(2, tracker.GetMostPopularContent());
    }
    
    [Fact]
    public void PopularityTracker_CanDecreasePopularity()
    {
        var tracker = new PopularityTracker();
        tracker.IncreasePopularity(1);
        tracker.IncreasePopularity(1);
        tracker.IncreasePopularity(1);
        Assert.Equal(1, tracker.GetMostPopularContent());
        tracker.IncreasePopularity(2);
        tracker.IncreasePopularity(2);
        Assert.Equal(1, tracker.GetMostPopularContent());
        tracker.DecreasePopularity(1);
        tracker.DecreasePopularity(1);
        Assert.Equal(2, tracker.GetMostPopularContent());
    }
    
    [Fact]
    public void PopularityTracker_CanHandleMultiplePopularity()
    {
        var tracker = new PopularityTracker();
        tracker.IncreasePopularity(1);
        tracker.IncreasePopularity(1);
        Assert.Equal(1, tracker.GetMostPopularContent());
        tracker.IncreasePopularity(2);
        tracker.IncreasePopularity(2);
        Assert.Equal(1, tracker.GetMostPopularContent());
        tracker.DecreasePopularity(1);
        tracker.IncreasePopularity(1);
        Assert.Equal(2, tracker.GetMostPopularContent());
    }
    
    [Fact]
    public void PopularityTracker_CanHandleEmptyCase()
    {
        var tracker = new PopularityTracker();
        Assert.Equal(-1, tracker.GetMostPopularContent());
    }
    
    [Fact]
    public void PopularityTracker_CanHandleEdgeCases()
    {
        var tracker = new PopularityTracker();
        tracker.DecreasePopularity(1);
        tracker.DecreasePopularity(1);
        Assert.Equal(-1, tracker.GetMostPopularContent());
    }
}