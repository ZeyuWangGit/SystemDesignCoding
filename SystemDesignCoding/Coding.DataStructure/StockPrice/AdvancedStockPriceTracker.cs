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
        if (!DateTime.TryParse(timestamp, out var time))
        {
            throw new ArgumentException("Invalid Timestamp");
        }

        if (_timestampToPriceList.Count == 0)
        {
            return -1;
        }
        var low = 0;
        var high = _timestampToPriceList.Count - 1;
        while (low <= high)
        {
            var mid = low + (high - low) / 2;
            if (_timestampToPriceList.Keys[mid] <= time)
            {
                low = mid + 1;
            }
            else
            {
                high = mid - 1;
            }
        }

        if (high < 0)
        {
            return -1;
        }
        return _timestampToPriceList.Values.Take(high + 1).Max();
    }
}