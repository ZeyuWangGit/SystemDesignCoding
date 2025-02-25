namespace Coding.CodeDesignTask;

public class StockPriceTracker
{
    private readonly Dictionary<DateTime, int> _timestampToPriceMap;
    private readonly SortedDictionary<int, int> _priceCountMap;
    private readonly SortedList<DateTime, int> _timestampToPriceList;
    private DateTime _latestTimestamp;

    public StockPriceTracker()
    {
        _timestampToPriceMap = new Dictionary<DateTime, int>();
        _priceCountMap = new SortedDictionary<int, int>();
        _timestampToPriceList = new SortedList<DateTime, int>();
        _latestTimestamp = DateTime.MinValue;
    }

    public void AddOrUpdate(string timestampStr, int price)
    {
        var timestamp = DateTime.Parse(timestampStr);
        
        // Check if it's an old price, if it's an old price, then check the _priceCountMap, delete record for old price
        if (_timestampToPriceMap.TryGetValue(timestamp, out var oldPrice))
        {
            if (_priceCountMap.ContainsKey(oldPrice) && _priceCountMap[oldPrice] == 1)
            {
                _priceCountMap.Remove(oldPrice);
            } else if (_priceCountMap.ContainsKey(oldPrice) && _priceCountMap[oldPrice] > 1)
            {
                _priceCountMap[oldPrice]--;
            }
        }

        // update _timestampToPriceMap
        _timestampToPriceMap[timestamp] = price;
        
        // update new priceCountMap
        _priceCountMap.TryAdd(price, 0);
        _priceCountMap[price]++;
        
        // update latesttimestamp
        if (timestamp > _latestTimestamp)
        {
            _latestTimestamp = timestamp;
        }
        
        // Add or update _timestampToPriceList
        _timestampToPriceList[timestamp] = price;
    }

    public int GetLatestPrice()
    {
        if (_timestampToPriceMap.Count == 0)
        {
            return -1;
        }
        return _timestampToPriceMap[_latestTimestamp];
    }

    public int GetMaxPrice()
    {
        if (_timestampToPriceMap.Count == 0)
        {
            return -1;
        }
        return _priceCountMap.Keys.Last();
    }

    public int GetMinPrice()
    {
        if (_timestampToPriceMap.Count == 0)
        {
            return -1;
        }
        return _priceCountMap.Keys.First();
    }

    public int GetMaximumUpTo(string timestampStr)
    {
        var timestamp = DateTime.Parse(timestampStr);
        var low = 0;
        var high = _timestampToPriceList.Count - 1;
        while (low <= high)
        {
            var mid = low + (high - low) / 2;
            if (_timestampToPriceList.Keys[mid] <= timestamp)
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

        return _timestampToPriceList.Values[high];
    }
    
    
}