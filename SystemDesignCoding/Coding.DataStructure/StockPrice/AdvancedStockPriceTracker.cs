namespace Coding.DataStructure.StockPrice;

public class AdvancedStockPriceTracker: IStockPriceTracker
{
    private readonly SortedList<DateTime, int> _timestampToPriceList = new();
    private readonly SortedDictionary<int, int> _priceToCountDict = new();
    
    public void AddOrUpdate(string timestamp, int price)
    {
        if (!DateTime.TryParse(timestamp, out var time))
        {
            throw new ArgumentException("Invalid Timestamp");
        }

        if (_timestampToPriceList.TryGetValue(time, out var oldPrice))
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

        _timestampToPriceList[time] = price;
        _priceToCountDict[price] = _priceToCountDict.GetValueOrDefault(price, 0) + 1;
    }

    public int GetMaxPrice()
    {
        return _priceToCountDict.Count == 0 ? -1 : _priceToCountDict.Keys.Last();
    }

    public int GetMaxPriceBefore(string timestamp)
    {
        if (!DateTime.TryParse(timestamp, out var parsedTime))
        {
            throw new ArgumentException("Invalid Timestamp");
        }

        if (_timestampToPriceList.Count == 0)
        {
            return -1;
        }

        var maxPrice = _timestampToPriceList.First().Value;
        foreach (var (time, price) in _timestampToPriceList)
        {
            if (time <= parsedTime)
            {
                maxPrice = Math.Max(maxPrice, price);
            }
        }
        return maxPrice;
    }
}