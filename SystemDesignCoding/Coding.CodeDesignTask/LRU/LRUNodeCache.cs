namespace Coding.CodeDesignTask;

public class LRUNodeCache
{
    private Dictionary<int, LRUNode> _keyToNodeDict = new Dictionary<int, LRUNode>();
    private LRUNode _head = new LRUNode();
    private LRUNode _tail = new LRUNode();
    private int _cap;

    public LRUNodeCache(int capacity)
    {
        _cap = capacity;
        _head.Right = _tail;
        _tail.Left = _head;
    }

    public int Get(int key)
    {
        if (!_keyToNodeDict.ContainsKey(key))
        {
            return -1;
        }

        var node = _keyToNodeDict[key];
        MoveToHead(node);
        return node.Value;
    }

    public void Put(int key, int value)
    {
        if (_keyToNodeDict.ContainsKey(key))
        {
            var node = _keyToNodeDict[key];
            node.Value = value;
            MoveToHead(node);
        }
        else
        {
            if (_keyToNodeDict.Count >= _cap)
            {
                RemoveLast();
            }

            var node = new LRUNode(key, value);
            _keyToNodeDict.Add(key, node);
            AddToHead(node);
        }
    }

    private void AddToHead(LRUNode node)
    {
        node.Left = _head;
        node.Right = _head.Right;
        node.Left.Right = node;
        node.Right.Left = node;
    }

    private void RemoveNode(LRUNode node)
    {
        if (node.Left != null)
        {
            node.Left.Right = node.Right;
        }

        if (node.Right != null)
        {
            node.Right.Left = node.Left;
        }
    }

    private void MoveToHead(LRUNode node)
    {
        RemoveNode(node);
        AddToHead(node);
    }

    private LRUNode RemoveLast()
    {
        if (_tail.Left != _head && _tail.Left != null)
        {
            var toRemove = _tail.Left;
            RemoveNode(toRemove);
            _keyToNodeDict.Remove(toRemove.Key);
            return toRemove;
        }

        throw new InvalidOperationException();
    }
}

public class LRUNode(int key = -1, int value = -1)
{
    public int Key { get; set; } = key;
    public int Value { get; set; } = value;
    public LRUNode? Left { get; set; } = null;
    public LRUNode? Right { get; set; } = null;
}