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
    [Fact]
    public void Test_EarliestAncestor_BasicCase()
    {
        int[][] edges =
        {
            new int[] {1, 4},
            new int[] {1, 5},
            new int[] {2, 5},
            new int[] {3, 6},
            new int[] {6, 7}
        };

        var expected = new HashSet<int> { 1, 2 }; // The farthest ancestors of node 5
        var result = NodeFinderCode.EarliestAncestor(edges, 5);
        
        Assert.Equal(expected, new HashSet<int>(result));
    }

    [Fact]
    public void Test_EarliestAncestor_SingleRoot()
    {
        int[][] edges =
        {
            new int[] {1, 2},
            new int[] {2, 3},
            new int[] {3, 4}
        };

        var expected = new HashSet<int> { 1 }; // The farthest ancestor of node 4
        var result = NodeFinderCode.EarliestAncestor(edges, 4);
        
        Assert.Equal(expected, new HashSet<int>(result));
    }

    [Fact]
    public void Test_EarliestAncestor_MultipleRoots()
    {
        int[][] edges =
        {
            new int[] {1, 3},
            new int[] {2, 3},
            new int[] {3, 5},
            new int[] {4, 5},
            new int[] {6, 7}
        };

        var expected = new HashSet<int> { 1, 2 }; // The farthest ancestors of node 5
        var result = NodeFinderCode.EarliestAncestor(edges, 5);
        
        Assert.Equal(expected, new HashSet<int>(result));
    }

    [Fact]
    public void Test_EarliestAncestor_NoAncestor()
    {
        int[][] edges =
        {
            new int[] {1, 2},
            new int[] {2, 3},
            new int[] {3, 4}
        };

        var expected = new HashSet<int>(); // Node 1 has no ancestors
        var result = NodeFinderCode.EarliestAncestor(edges, 1);
        
        Assert.Equal(expected, new HashSet<int>(result));
    }

    [Fact]
    public void Test_EarliestAncestor_DisconnectedGraph()
    {
        int[][] edges =
        {
            new int[] {1, 2},
            new int[] {3, 4},
            new int[] {5, 6}
        };

        var expected = new HashSet<int> { 3 }; // The farthest ancestor of node 4
        var result = NodeFinderCode.EarliestAncestor(edges, 4);
        
        Assert.Equal(expected, new HashSet<int>(result));
    }
}