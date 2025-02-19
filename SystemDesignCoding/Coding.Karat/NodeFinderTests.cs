namespace Coding.Karat;

public class NodeFinderTests
{
    [Fact]
    public void Test_FindNodesWithZeroOrOneParent()
    {
        int[][] edges = new int[][]
        {
            new int[] {1, 4},
            new int[] {1, 5},
            new int[] {2, 5},
            new int[] {3, 6},
            new int[] {6, 7}
        };

        List<int> expected = new List<int> {1, 2, 3, 4, 6, 7};
        List<int> result = NodeFinderCode.FindNodesWithZeroOrOneParent(edges);
        result.Sort();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_EmptyEdges()
    {
        int[][] edges = new int[][] { };

        List<int> expected = new List<int> { };
        List<int> result = NodeFinderCode.FindNodesWithZeroOrOneParent(edges);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_AllNodesWithMultipleParents()
    {
        int[][] edges = new int[][]
        {
            new int[] {1, 3},
            new int[] {2, 3},
            new int[] {3, 4},
            new int[] {4, 5},
            new int[] {5, 6},
            new int[] {6, 7}
        };

        List<int> expected = new List<int> {1, 2, 4, 5, 6, 7};
        List<int> result = NodeFinderCode.FindNodesWithZeroOrOneParent(edges);

        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void Test_HasCommonAncestor()
    {
        int[][] edges = new int[][]
        {
            new int[] {1, 4},
            new int[] {1, 5},
            new int[] {2, 5},
            new int[] {3, 6},
            new int[] {6, 7}
        };

        Assert.True(NodeFinderCode.HasCommonAncestor(edges, 4, 5));
        Assert.False(NodeFinderCode.HasCommonAncestor(edges, 4, 6));
        Assert.False(NodeFinderCode.HasCommonAncestor(edges, 5, 7));
        Assert.False(NodeFinderCode.HasCommonAncestor(edges, 1, 7));
    }
}