namespace Coding.DataStructure.StockPrice;

public class AdvancedStockPriceTrackerTests
{
    [Fact]
    public void AdvancedStockPriceTracker_SingleEntry()
    {
        var tracker = new AdvancedStockPriceTracker();
        tracker.AddOrUpdate("2025-03-04 11:00", 100);
        Assert.Equal(100, tracker.GetMaxPrice());
    }
    
    [Fact]
    public void AdvancedStockPriceTracker_MulyipleEntries()
    {
        var tracker = new AdvancedStockPriceTracker();
        tracker.AddOrUpdate("2025-03-04 11:00", 100);
        tracker.AddOrUpdate("2025-03-04 12:00", 300);
        tracker.AddOrUpdate("2025-03-04 14:00", 400);
        Assert.Equal(400, tracker.GetMaxPrice());
    }
    
    [Fact]
    public void AdvancedStockPriceTracker_UpdateExistingTimestamp()
    {
        var tracker = new AdvancedStockPriceTracker();
        tracker.AddOrUpdate("2025-03-04 11:00", 500);
        tracker.AddOrUpdate("2025-03-04 12:00", 300);
        tracker.AddOrUpdate("2025-03-04 11:00", 400);
        Assert.Equal(400, tracker.GetMaxPrice());
    }
    
    [Fact]
    public void AdvancedStockPriceTracker_CanGetMaximumUpTo()
    {
        var tracker = new AdvancedStockPriceTracker();
        tracker.AddOrUpdate("2025-03-04 11:00", 500);
        tracker.AddOrUpdate("2025-03-04 12:00", 300);
        tracker.AddOrUpdate("2025-03-04 11:00", 400);
        tracker.AddOrUpdate("2025-03-04 13:00", 100);
        tracker.AddOrUpdate("2025-03-04 14:00", 600);
        tracker.AddOrUpdate("2025-03-04 15:00", 700);
        Assert.Equal(700, tracker.GetMaxPrice());
        Assert.Equal(400, tracker.GetMaxPriceBefore("2025-03-04 13:00"));
        Assert.Equal(600, tracker.GetMaxPriceBefore("2025-03-04 14:00"));
        Assert.Equal(700, tracker.GetMaxPriceBefore("2025-03-04 15:00"));
    }
    
}