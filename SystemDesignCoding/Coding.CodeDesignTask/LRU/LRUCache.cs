namespace Coding.CodeDesignTask;

public class LRUCache {
    private Cache _cache = new Cache();
    private int _cap;

    public LRUCache(int capacity) {
        _cap = capacity;
    }
    
    public int Get(int key) {
        if(_cache.ContainsKey(key)) {
            var val = _cache.GetValueByKey(key);
            _cache.Remove(key);
            _cache.Add(key, val);
            return val;
        }
        return -1;
    }
    
    public void Put(int key, int value) {
        if(_cache.ContainsKey(key)) {
            _cache.Remove(key);
            _cache.Add(key, value);
            return;
        }

        if (_cache.GetSize() >= _cap)
        {
            _cache.Remove(_cache.GetFirstKey());
        }
        _cache.Add(key, value);
    }
}

public class Cache {
    private Dictionary<int, LinkedListNode<(int key, int value)>> dict = new Dictionary<int, LinkedListNode<(int key, int value)>>();
    private LinkedList<(int key, int value)> list = new LinkedList<(int key, int value)>();

    public int GetSize() {
        return dict.Count;
    }
    public int GetValueByKey(int key) {
        return dict[key].Value.value;
    }
    public bool ContainsKey(int key) {
        return dict.ContainsKey(key);
    }
    public int GetFirstKey() {
        if (list.First == null || list.Count == 0)
        {
            return -1;
        }
        return list.First.Value.key;
    }
    public void Remove(int key) {
        if(!ContainsKey(key)) {
            return;
        }
        var node = dict[key];
        list.Remove(node);
        dict.Remove(key);
    }
    public void Add(int key, int value) {
        var node = new LinkedListNode<(int key, int value)>((key, value));
        list.AddLast(node);
        dict.Add(key, node);
    }

}