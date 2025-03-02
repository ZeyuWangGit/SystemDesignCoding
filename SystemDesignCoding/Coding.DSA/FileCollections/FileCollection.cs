namespace Coding.DSA.FileCollections;

public class FileCollection: IFileCollection
{
    private readonly Dictionary<string, int> _collectionSizeMap = new();
    private readonly Dictionary<string, string> _collectionParentMap = new();
    private int _totalSize = 0;
    
    public void AddCollectionHierarchy(string collection, string parentCollection)
    {
        if (!_collectionParentMap.ContainsKey(collection))
        {
            _collectionParentMap.Add(collection, parentCollection);
        }
    }

    public void AddFile(FileRecord record)
    {
        _totalSize += record.FileSize;

        foreach (var collection in record.Collections)
        {
            UpdateCollectionSize(collection, record.FileSize, []);
        }
    }

    private void UpdateCollectionSize(string collection, int size, HashSet<string> visited)
    {
        if (visited.Contains(collection))
        {
            return;
        }
        visited.Add(collection);

        if (!_collectionSizeMap.ContainsKey(collection))
        {
            _collectionSizeMap.Add(collection, 0);
        }
        _collectionSizeMap[collection] += size;
        if (_collectionParentMap.ContainsKey(collection))
        {
            var parent = _collectionParentMap[collection];
            UpdateCollectionSize(parent, size, visited);
        }
    }

    public int GetTotalSize()
    {
        return _totalSize;
    }

    public List<string> GetTopNCollections(int n)
    {
        var pq = new PriorityQueue<(string collectionName, int size), int>();
        foreach (var (collection, size) in _collectionSizeMap)
        {
            pq.Enqueue((collection, size), size);
            while (pq.Count > n)
            {
                pq.Dequeue();
            }
        }
        var result = new List<string>();
        while (pq.Count > 0)
        {
            var (collection, size) = pq.Dequeue();
            result.Add($"{collection}-{size}");
        }
        result.Reverse();
        return result;
    }
}