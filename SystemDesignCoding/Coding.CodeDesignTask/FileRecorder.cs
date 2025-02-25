namespace Coding.CodeDesignTask;

public class FileRecorder
{
    private int TotalSize = 0;
    private Dictionary<string, int> CollectionToSizeMap;
    private Dictionary<string, string> CollectionChildToParentMap;

    public FileRecorder()
    {
        CollectionToSizeMap = new Dictionary<string, int>();
        CollectionChildToParentMap = new Dictionary<string, string>();
    }

    public void AddCollectionHierarchy(string collection, string parentCollection)
    {
        if (!CollectionChildToParentMap.ContainsKey(collection))
        {
            CollectionChildToParentMap[collection] = parentCollection;
        }
    }

    public void AddFile(FileRecord record)
    {
        TotalSize += record.Size;

        foreach (var collection in record.Collections)
        {
            UpdateCollectionSize(collection, record.Size, new HashSet<string>());
        }
    }

    private void UpdateCollectionSize(string collection, int size, HashSet<string> visited)
    {
        if (visited.Contains(collection))
        {
            throw new InvalidOperationException($"Circular dependency detected at {collection}");
        }
        if (!CollectionToSizeMap.ContainsKey(collection))
        {
            CollectionToSizeMap.Add(collection, 0);
        }

        CollectionToSizeMap[collection] += size;

        if (CollectionChildToParentMap.ContainsKey(collection))
        {
            var parent = CollectionChildToParentMap[collection];
            UpdateCollectionSize(parent, size, visited);
        }
    }

    public int GetTotalSize()
    {
        return TotalSize;
    }

    public List<string> GetTopNCollections(int n)
    {
        var pq = new PriorityQueue<(string collection, int size), int>(Comparer<int>.Create((a, b) => a.CompareTo(b)));
        foreach (var pair in CollectionToSizeMap)
        {
            var collection = pair.Key;
            var size = pair.Value;
            pq.Enqueue((collection, size), size);
            if (pq.Count > n)
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

public class FileRecord
{
    public string Name { get; set; }
    public int Size { get; set; }
    public List<string> Collections { get; set; }

    public FileRecord(string name, int size, List<string>? collections = null)
    {
        Name = name;
        Size = size;
        Collections = collections ?? [];
    }
    
}