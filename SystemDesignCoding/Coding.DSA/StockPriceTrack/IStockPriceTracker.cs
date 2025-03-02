namespace Coding.DSA.StockPriceTrack;

public interface IStockPriceTracker
{
    void AddOrUpdate(string timestamp, int price);
    int GetLatestPrice();
    int GetMaxPrice();
    int GetMinPrice();
    int GetMaximumUpTo(string timestamp);
}