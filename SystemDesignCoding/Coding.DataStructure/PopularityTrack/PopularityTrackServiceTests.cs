namespace Coding.DataStructure.PopularityTrack;

public class PopularityTrackServiceTests
{
    [Fact]
    public void PopularityTrackService_CanIncreasePopularity()
    {
        var tracker = new PopularityTrackService();
        tracker.IncreasePopularity(1);
        Assert.Equal(1, tracker.GetMostPopularContent());
        tracker.IncreasePopularity(2);
        tracker.IncreasePopularity(2);
        Assert.Equal(2, tracker.GetMostPopularContent());
    }
    
    [Fact]
    public void PopularityTrackService_CanDecreasePopularity()
    {
        var tracker = new PopularityTrackService();
        tracker.IncreasePopularity(1);
        tracker.IncreasePopularity(1);
        tracker.IncreasePopularity(1);
        tracker.IncreasePopularity(2);
        tracker.IncreasePopularity(2);
        Assert.Equal(1, tracker.GetMostPopularContent());
        tracker.DecreasePopularity(1);
        tracker.DecreasePopularity(1);
        Assert.Equal(2, tracker.GetMostPopularContent());
    }
    
    [Fact]
    public void PopularityTrackService_CanHandleMultiplePopularity()
    {
        var tracker = new PopularityTrackService();
        tracker.IncreasePopularity(1);
        tracker.IncreasePopularity(1);
        tracker.IncreasePopularity(2);
        tracker.IncreasePopularity(2);
        Assert.Equal(1, tracker.GetMostPopularContent());
        tracker.DecreasePopularity(1);
        tracker.IncreasePopularity(1);
        Assert.Equal(2, tracker.GetMostPopularContent());
    }
    
    [Fact]
    public void PopularityTrackService_CanHandleEmpty()
    {
        var tracker = new PopularityTrackService();
        Assert.Equal(-1, tracker.GetMostPopularContent());
    }
    
    [Fact]
    public void PopularityTrackService_CanHandleEdgeCases()
    {
        var tracker = new PopularityTrackService();
        Assert.Throws<ArgumentException>(() => tracker.DecreasePopularity(1));
    }
}