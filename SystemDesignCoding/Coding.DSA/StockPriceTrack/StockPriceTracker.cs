namespace Coding.DSA.StockPriceTrack;

public class StockPriceTracker: IStockPriceTracker
{
    private readonly Dictionary<DateTime, int> _timeToPriceDict = new();
    private readonly SortedDictionary<int, int> _priceCountDict = new();
    private readonly SortedList<DateTime, int> _timeToPriceList = new();
    private DateTime _latestTimestamp = DateTime.MinValue;
    
    public void AddOrUpdate(string timestamp, int price)
    {
        var time = DateTime.Parse(timestamp);
        if (_timeToPriceDict.ContainsKey(time))
        {
            var prevPrice = _timeToPriceDict[time];
            if (_priceCountDict.ContainsKey(prevPrice))
            {
                if (_priceCountDict[prevPrice] <= 1)
                {
                    _priceCountDict.Remove(prevPrice);
                }
                else
                {
                    _priceCountDict[prevPrice]--;
                }
            }

            _timeToPriceDict[time] = price;
        }
        else
        {
            _timeToPriceDict.Add(time, price);
        }
        
        if (_priceCountDict.ContainsKey(price))
        {
            _priceCountDict[price] = price + 1;
        }
        else
        {
            _priceCountDict.Add(price, 1);
        }
        if (time > _latestTimestamp)
        {
            _latestTimestamp = time;
        }

        if (_timeToPriceList.ContainsKey(time))
        {
            _timeToPriceList[time] = price;
        }
        else
        {
            _timeToPriceList.Add(time, price);    
        }
    }

    public int GetLatestPrice()
    {
        if (_timeToPriceDict.Count == 0)
        {
            return -1;
        }

        return _timeToPriceDict[_latestTimestamp];
    }

    public int GetMaxPrice()
    {
        if (_timeToPriceDict.Count == 0)
        {
            return -1;
        }

        return _priceCountDict.Keys.Last();
    }

    public int GetMinPrice()
    {
        if (_timeToPriceDict.Count == 0)
        {
            return -1;
        }
        return _priceCountDict.Keys.First();
    }

    public int GetMaximumUpTo(string timestamp)
    {
        var low = 0;
        var high = _timeToPriceList.Count - 1;
        var time = DateTime.Parse(timestamp);
        while (low <= high)
        {
            var mid = low + (high - low) / 2;
            if (_timeToPriceList.Keys[mid] <= time)
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

        return _timeToPriceList.Values[high];
    }
}