namespace Coding.DataStructure.StockPrice;

public class BasicStockPriceTracker: IStockPriceTracker
{
    private readonly Dictionary<DateTime, int> _timestampToPriceDict = new();
    private readonly SortedDictionary<int, int> _priceToCountDict = new();
    
    public void AddOrUpdate(string timestamp, int price)
    {
        if (!DateTime.TryParse(timestamp, out var time))
        {
            throw new ArgumentException("Invalid Timestamp");
        }

        if (_timestampToPriceDict.TryGetValue(time, out var oldPrice))
        {
            if (_priceToCountDict[oldPrice] == 1)
            {
                _priceToCountDict.Remove(oldPrice);
            }
            else
            {
                _priceToCountDict[oldPrice]--;
            }
        }

        _timestampToPriceDict[time] = price;
        _priceToCountDict[price] = _priceToCountDict.GetValueOrDefault(price, 0) + 1;
    }

    public int GetMaxPrice()
    {
        return _priceToCountDict.Count == 0 ? -1 : _priceToCountDict.Keys.Last();
    }

    public int GetMaxPriceBefore(string timestamp)
    {
        throw new NotImplementedException();
    }
}