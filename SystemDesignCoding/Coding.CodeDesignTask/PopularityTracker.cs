namespace Coding.CodeDesignTask;

public class PopularityTracker
{
    private readonly Node _headNode = new Node(0);
    private readonly Node _tailNode = new Node(0);
    private readonly Dictionary<int, Node> _contentIdNodeMap = new Dictionary<int, Node>();

    public PopularityTracker()
    {
        _headNode.Right = _tailNode;
        _tailNode.Left = _headNode;
    }
    
    public void IncreasePopularity(int contentId)
    {
        if (_contentIdNodeMap.ContainsKey(contentId))
        {
            var node = _contentIdNodeMap[contentId];
            node.Set.Remove(contentId);
            var count = node.Count;
            Node nextNode;
            if (node.Right != null && node.Right.Count == count + 1)
            {
                nextNode = node.Right;
            }
            else
            {
                nextNode = new Node(count + 1);
                nextNode.Right = node.Right;
                nextNode.Left = node;
                if (nextNode.Right != null)
                {
                    nextNode.Right.Left = nextNode;    
                }
                nextNode.Left.Right = nextNode;
            }

            nextNode.Set.Add(contentId);
            _contentIdNodeMap[contentId] = nextNode;
            ClearNode(node);
        }
        else
        {
            Node node;
            if (_headNode.Right != null && _headNode.Right.Count == 1)
            {
                node = _headNode.Right;
            }
            else
            {
                node = new Node(1);
                node.Right = _headNode.Right;
                node.Left = _headNode;
                if (node.Right != null)
                {
                    node.Right.Left = node;    
                }

                node.Left.Right = node;
            }

            node.Set.Add(contentId);
            _contentIdNodeMap.Add(contentId, node);
        }
    }

    public void DecreasePopularity(int contentId)
    {
        if (!_contentIdNodeMap.ContainsKey(contentId))
        {
            return;
        }
        var node = _contentIdNodeMap[contentId];
        node.Set.Remove(contentId);
        var count = node.Count;
        if (count == 1)
        {
            _contentIdNodeMap.Remove(contentId);
        }
        else
        {
            if (node.Left != null && node.Left.Count == count - 1)
            {
                node.Left.Set.Add(contentId);
                _contentIdNodeMap[contentId] = node.Left;
            }
            else
            {
                var prev = new Node(count - 1);
                prev.Right = node;
                prev.Left = node.Left;
                prev.Right.Left = prev;
                prev.Left.Right = prev;
                prev.Set.Add(contentId);
                _contentIdNodeMap[contentId] = prev;
            }
        }

        ClearNode(node);
    }

    public int GetMostPopular()
    {
        if (_tailNode.Left == _headNode)
        {
            return -1;
        }

        var node = _tailNode.Left;
        foreach (var item in node.Set)
        {
            return item;
        }

        return -1;
    }

    private void ClearNode(Node node)
    {
        if (node.Set.Count == 0)
        {
            if (node.Right != null)
            {
                node.Right.Left = node.Left;
            }

            if (node.Left != null)
            {
                node.Left.Right = node.Right;
            }
        }
    }
}

public class Node(int count)
{
    public int Count { get; set; } = count;
    public readonly HashSet<int> Set = [];
    public Node? Left { get; set; }
    public Node? Right { get; set; }
}