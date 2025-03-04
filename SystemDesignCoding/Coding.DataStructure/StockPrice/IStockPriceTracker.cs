namespace Coding.DataStructure.StockPrice;

public interface IStockPriceTracker
{
    void AddOrUpdate(string timestamp, int price);
    int GetMaxPrice();
    int GetMaxPriceBefore(string timestamp);
}