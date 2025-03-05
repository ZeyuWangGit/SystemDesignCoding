namespace Coding.DataStructure.PopularityTrack;

public class PopularityTrackService: IPopularityTrackService
{
    private Dictionary<int, PopularityNode> _contentIdToNodeDict = new();
    private PopularityNode _head = new(-100);
    private PopularityNode _tail = new(-100);

    public PopularityTrackService()
    {
        _head.Right = _tail;
        _tail.Left = _head;
    }
    
    public void IncreasePopularity(int contentId)
    {
        if (_contentIdToNodeDict.ContainsKey(contentId))
        {
            var node = _contentIdToNodeDict[contentId];
            var currCount = node.Count;
            node.ContentIds.Remove(contentId);
            if (node.Right != null && node.Right.Count == currCount + 1)
            {
                var nextNode = node.Right;
                nextNode.ContentIds.AddLast(contentId);
                _contentIdToNodeDict[contentId] = nextNode;
            }
            else if(node.Right != null && node.Right.Count != currCount + 1)
            {
                var nextNode = new PopularityNode(currCount + 1);
                nextNode.Right = node.Right;
                nextNode.Left = node;
                nextNode.Left.Right = nextNode;
                nextNode.Right.Left = nextNode;
                nextNode.ContentIds.AddLast(contentId);
                _contentIdToNodeDict[contentId] = nextNode;
            }
            ClearNode(node);
        }
        else
        {
            if (_head.Right != null && _head.Right.Count == 1)
            {
                var nextNode = _head.Right;
                nextNode.ContentIds.AddLast(contentId);
                _contentIdToNodeDict.Add(contentId, nextNode);
            } else if (_head.Right != null && _head.Right.Count != 1)
            {
                var nextNode = new PopularityNode(1);
                nextNode.Left = _head;
                nextNode.Right = _head.Right;
                nextNode.Left.Right = nextNode;
                nextNode.Right.Left = nextNode;
                nextNode.ContentIds.AddLast(contentId);
                _contentIdToNodeDict.Add(contentId, nextNode);
            }
        }
    }

    public void DecreasePopularity(int contentId)
    {
        if (!_contentIdToNodeDict.ContainsKey(contentId))
        {
            throw new ArgumentException("ContentId not exist");
        }

        var node = _contentIdToNodeDict[contentId];
        var count = node.Count;
        node.ContentIds.Remove(contentId);
        if (count == 1)
        {
            _contentIdToNodeDict.Remove(contentId);
        }
        else
        {
            if (node.Left != null && node.Left.Count == count - 1)
            {
                var prevNode = node.Left;
                prevNode.ContentIds.AddLast(contentId);
                _contentIdToNodeDict[contentId] = prevNode;
            } else if (node.Left != null && node.Left.Count != count - 1)
            {
                var prevNode = new PopularityNode(count - 1);
                prevNode.Left = node.Left;
                prevNode.Right = node;
                prevNode.Left.Right = prevNode;
                prevNode.Right.Left = prevNode;
                prevNode.ContentIds.AddLast(contentId);
                _contentIdToNodeDict[contentId] = prevNode;
            }
        }
        ClearNode(node);

    }

    public int GetMostPopularContent()
    {
        if (_tail.Left != null && _tail.Left != _head && _tail.Left.ContentIds.Count > 0)
        {
            return _tail.Left.ContentIds.First();
        }

        return -1;
    }

    private void ClearNode(PopularityNode node)
    {
        if (node.ContentIds.Count == 0)
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

public class PopularityNode(int count)
{
    public int Count { get; set; } = count;
    public LinkedList<int> ContentIds { get; set; } = [];
    public PopularityNode? Left { get; set; }
    public PopularityNode? Right { get; set; }
}