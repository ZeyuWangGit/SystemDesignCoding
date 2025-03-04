namespace Coding.DataStructure.StockPrice;

public class BasicStockPriceTrackerTests
{
    [Fact]
    public void BasicStockPriceTracker_SingleEntry()
    {
        var tracker = new BasicStockPriceTracker();
        tracker.AddOrUpdate("2025-03-04 11:00", 100);
        Assert.Equal(100, tracker.GetMaxPrice());
    }
    
    [Fact]
    public void BasicStockPriceTracker_MulyipleEntries()
    {
        var tracker = new BasicStockPriceTracker();
        tracker.AddOrUpdate("2025-03-04 11:00", 100);
        tracker.AddOrUpdate("2025-03-04 12:00", 300);
        tracker.AddOrUpdate("2025-03-04 14:00", 400);
        Assert.Equal(400, tracker.GetMaxPrice());
    }
    
    [Fact]
    public void BasicStockPriceTracker_UpdateExistingTimestamp()
    {
        var tracker = new BasicStockPriceTracker();
        tracker.AddOrUpdate("2025-03-04 11:00", 500);
        tracker.AddOrUpdate("2025-03-04 12:00", 300);
        tracker.AddOrUpdate("2025-03-04 11:00", 400);
        Assert.Equal(400, tracker.GetMaxPrice());
    }
}