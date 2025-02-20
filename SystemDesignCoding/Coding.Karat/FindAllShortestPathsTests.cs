namespace Coding.Karat;

public class FindAllShortestPathsTests
{
    [Fact]
    public void Test1_ValidPathExists()
    {
        int[][] board = new int[][]
        {
            new int[] { 1, 0, 0, 0, 0 },
            new int[] { 0, -1, -1, 0, 0 },
            new int[] { 0, -1, 0, 1, 0 },
            new int[] { -1, 0, 0, 0, 0 },
            new int[] { 0, 1, -1, 0, 0 },
            new int[] { 0, 0, 0, 0, 0 },
        };
        var start = (5, 2);
        var end = (2, 0);

        var result = FindAllShortestPathsCode.FindAllShortestPaths(board, start, end);

        Assert.NotEmpty(result); // 至少有一个合法路径
        Assert.All(result, path => Assert.True(path.Count <= 14)); // 确保所有路径都是最短的
    }

    [Fact]
    public void Test2_NoPathExists()
    {
        int[][] board = new int[][]
        {
            new int[] { 1, 0, -1, 0, 0 },
            new int[] { 0, -1, -1, 0, 0 },
            new int[] { 0, -1, 0, 1, 0 },
            new int[] { -1, 0, 0, 0, 0 },
            new int[] { 0, 1, -1, 0, 0 },
            new int[] { 0, 0, 0, 0, 0 },
        };
        var start = (5, 0);
        var end = (0, 4);

        var result = FindAllShortestPathsCode.FindAllShortestPaths(board, start, end);

        Assert.Empty(result); // 没有可行路径
    }
    
}