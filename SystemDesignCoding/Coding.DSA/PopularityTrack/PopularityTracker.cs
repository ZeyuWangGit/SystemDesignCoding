namespace Coding.DSA.PopularityTrack;

public class PopularityTracker: IPopularityTracker
{
    private readonly Dictionary<int, PopularityNode> _idToNodeDict;
    private readonly PopularityNode _head;
    private readonly PopularityNode _tail;
    
    public PopularityTracker()
    {
        _idToNodeDict = new Dictionary<int, PopularityNode>();
        _head = new PopularityNode(-1000);
        _tail = new PopularityNode(-1000);
        _head.Right = _tail;
        _tail.Left = _head;
    }

    public void IncreasePopularity(int contentId)
    {
        if (_idToNodeDict.ContainsKey(contentId))
        {
            var node = _idToNodeDict[contentId];
            node.ContentIds.Remove(contentId);
            var count = node.PopularityCount;
            
            if (node.Right != null && node.Right.PopularityCount == count + 1)
            {
                var nextNode = node.Right;
                nextNode.ContentIds.AddLast(contentId);
                _idToNodeDict[contentId] = nextNode;

            }
            else if (node.Right != null && node.Right.PopularityCount != count + 1)
            {
                var nextNode = new PopularityNode(count + 1)
                {
                    Left = node,
                    Right = node.Right
                };
                nextNode.Left.Right = nextNode;
                nextNode.Right.Left = nextNode;
                nextNode.ContentIds.AddLast(contentId);
                _idToNodeDict[contentId] = nextNode;
            }
            ClearNode(node);
        }
        else
        {
            if (_head.Right != null && _head.Right.PopularityCount == 1)
            {
                var node = _head.Right;
                node.ContentIds.AddLast(contentId);
                _idToNodeDict[contentId] = node;
            } else if (_head.Right != null && _head.Right.PopularityCount != 1)
            {
                var node = new PopularityNode(1);
                node.Left = _head;
                node.Right = _head.Right;
                node.Left.Right = node;
                node.Right.Left = node;
                node.ContentIds.AddLast(contentId);
                _idToNodeDict[contentId] = node;
            }
        }
    }

    public void DecreasePopularity(int contentId)
    {
        if (!_idToNodeDict.ContainsKey(contentId))
        {
            return;
        }
        var node = _idToNodeDict[contentId];
        node.ContentIds.Remove(contentId);
        var count = node.PopularityCount;
        if (count == 1)
        {
            _idToNodeDict.Remove(contentId);
        }
        else
        {
            if (node.Left != null && node.Left.PopularityCount == count - 1)
            {
                var prevNode = node.Left;
                prevNode.ContentIds.AddLast(contentId);
                _idToNodeDict[contentId] = prevNode;
            } else if (node.Left != null && node.Left.PopularityCount != count - 1)
            {
                var prevNode = new PopularityNode(count - 1);
                prevNode.Left = node.Left;
                prevNode.Right = node;
                prevNode.Left.Right = prevNode;
                prevNode.Right.Left = prevNode;
                prevNode.ContentIds.AddLast(contentId);
                _idToNodeDict[contentId] = prevNode;
            }
        }
        ClearNode(node);
    }

    public int GetMostPopularContent()
    {
        if (_tail.Left != null && _tail.Left != _head)
        {
            return _tail.Left.ContentIds.First();
        }

        return -1;
    }

    private void ClearNode(PopularityNode node)
    {
        if (node.ContentIds.Count == 0)
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

public class PopularityNode(int popularityCount)
{
    public int PopularityCount { get; set; } = popularityCount;
    public LinkedList<int> ContentIds { get; set; } = [];
    public PopularityNode? Left { get; set; }
    public PopularityNode? Right { get; set; }
}