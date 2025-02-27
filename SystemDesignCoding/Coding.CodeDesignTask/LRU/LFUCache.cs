namespace Coding.CodeDesignTask;

public class LFUCache
{
    private Dictionary<int, int> _keyToValueMap = new Dictionary<int, int>();
    private Dictionary<int, CountNode> _keyToNodeMap = new Dictionary<int, CountNode>();
    private CountNode _head = new CountNode(-1);
    private CountNode _tail = new CountNode(-1);
    private int _cap;
    
    public LFUCache(int capacity)
    {
        _head.Right = _tail;
        _tail.Left = _head;
        _cap = capacity;
    }
    
    public int Get(int key) 
    {
        if (!_keyToValueMap.ContainsKey(key))
        {
            return -1;
        }

        var value = _keyToValueMap[key];
        IncreaseCount(key);
        return value;
    }
    
    public void Put(int key, int value) 
    {
        if (_keyToValueMap.Count == _cap)
        {
            RemoveLessFrequent();
        }
        if (_keyToValueMap.ContainsKey(key))
        {
            _keyToValueMap[key] = value;
        }
        else
        {
            _keyToValueMap.Add(key, value);
        }
        IncreaseCount(key);
    }

    public void IncreaseCount(int key)
    {
        if (_keyToNodeMap.ContainsKey(key))
        {
            var node = _keyToNodeMap[key];
            var count = node.Count;
            node.Keys.Remove(key);
            CountNode next;
            if (node.Right != null && node.Right.Count == count + 1)
            {
                next = node.Right;
            }
            else
            {
                next = new CountNode(count + 1);
                next.Right = node.Right;
                next.Left = node;
                next.Right.Left = next;
                next.Left.Right = next;
            }

            next.Keys.AddLast(key);
            _keyToNodeMap[key] = next;
            ClearNode(node);
        }
        else
        {
            CountNode node;
            if (_head.Right != null && _head.Right.Count == 1)
            {
                node = _head.Right;
            }
            else
            {
                node = new CountNode(1);
                node.Right = _head.Right;
                node.Left = _head;
                if (node.Right != null)
                {
                    node.Right.Left = node;    
                }

                node.Left.Right = node;
            }

            node.Keys.AddLast(key);
            _keyToNodeMap.Add(key, node);
        }
    }

    public void ClearNode(CountNode node)
    {
        if (node.Keys.Count == 0 && node != _head && node != _tail)
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
    }

    public void RemoveLessFrequent()
    {
        if (_head.Right == _tail || _head.Right == null || _head.Right.Keys.First == null)
        {
            throw new InvalidOperationException();
        }
        var node = _head.Right;
        var key = node.Keys.First.Value;
        _keyToValueMap.Remove(key);
        _keyToNodeMap.Remove(key);
        node.Keys.Remove(key);
        ClearNode(node);
    }
}

public class CountNode(int count)
{
    public int Count { get; set; } = count;
    public CountNode? Left { get; set; }
    public CountNode? Right { get; set; }
    public LinkedList<int> Keys { get; set; } = new();

}
