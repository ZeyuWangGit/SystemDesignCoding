namespace Coding.CodeDesign;

public class PopularityTracker
{
    public void IncreasePopularity(int contentId)
    {
        throw new NotImplementedException();
    }

    public void DecreasePopulatiry(int conentId)
    {
        throw new NotImplementedException();
    }

    public int GetMostPopular()
    {
        throw new NotImplementedException();
    }
}

public class Node(int count)
{
    public int Count { get; set; } = count;
    public HashSet<int> Set = [];
    public Node? Left { get; set; }
    public Node? Right { get; set; }
}