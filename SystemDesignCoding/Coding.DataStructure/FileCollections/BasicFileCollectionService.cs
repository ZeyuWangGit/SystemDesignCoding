namespace Coding.DataStructure.FileCollections;

public class BasicFileCollectionService
{
    private readonly Dictionary<string, int> _collectionNameToSizeDict = new();
    private int _totalsize = 0;
    
    public void AddFile(string fileName, int fileSize, string[] collections)
    {
        foreach (var collection in collections)
        {
            _collectionNameToSizeDict.TryAdd(collection, 0);
            _collectionNameToSizeDict[collection] += fileSize;
        }

        _totalsize += fileSize;
    }

    public int GetTotalSize()
    {
        return _totalsize;
    }

    public List<string> GetTopNCollections(int n)
    {
        var pq = new PriorityQueue<(string collectionName, int size), (string collectionName, int size)>(
            Comparer<(string collectionName, int size)>.Create((a, b) =>
            {
                if (a.size != b.size)
                {
                    return b.size.CompareTo(a.size);
                }

                return string.Compare(a.collectionName, b.collectionName, StringComparison.Ordinal);
            }));
        foreach (var (name, size) in _collectionNameToSizeDict)
        {
            pq.Enqueue((name, size), (name, size));
        }

        var res = new List<string>();
        for (int i = 0; i < n; i++)
        {
            if (pq.Count > 0)
            {
                var (name, size) = pq.Dequeue();
                res.Add($"{name}-{size}");    
            }
        }

        return res;
    }
}