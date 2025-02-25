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
            node.ContentList.Remove(contentId);
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

            nextNode.ContentList.AddLast(contentId);
            _contentIdNodeMap[contentId] = nextNode;
            ClearNode(node);
        }
        else
        {
            Node node;
            if (_headNode.Right != _tailNode && _headNode.Right != null && _headNode.Right.Count == 1)
                
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

            node.ContentList.AddLast(contentId);
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
        node.ContentList.Remove(contentId);
        var count = node.Count;
        if (count == 1)
        {
            _contentIdNodeMap.Remove(contentId);
        }
        else
        {
            if (node.Left != _headNode && node.Left != null && node.Left.Count == count - 1)
            {
                node.Left.ContentList.AddLast(contentId);
                _contentIdNodeMap[contentId] = node.Left;
            }
            else
            {
                var prev = new Node(count - 1);
                prev.Right = node;
                prev.Left = node.Left;
                if (prev.Right != null) prev.Right.Left = prev;
                if (prev.Left != null) prev.Left.Right = prev;
                prev.ContentList.AddLast(contentId);
                _contentIdNodeMap[contentId] = prev;
            }
        }

        ClearNode(node);
    }

    public int GetMostPopular()
    {
        var node = _tailNode.Left;
        while (node != _headNode && node.ContentList.Count == 0)
        {
            node = node.Left;
        }

        return (node == _headNode || node.ContentList.Count == 0) ? -1 : node.ContentList.First.Value;
    }

    private void ClearNode(Node node)
    {
        if (node.ContentList.Count == 0 && node != _headNode && node != _tailNode)
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
}

public class Node(int count)
{
    public int Count { get; set; } = count;
    public LinkedList<int> ContentList { get; set; } = new();
    public Node? Left { get; set; }
    public Node? Right { get; set; }
}