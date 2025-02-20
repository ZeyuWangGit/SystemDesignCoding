namespace Coding.Karat;

public class BestDayToCrossTests
{
    [Fact]
    public void TestExample1()
    {
        int[] altitudes = { 0, 1, 2, 1 };
        int[][] forecasts = {
            new int[] { 1, 0, 1, 0 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 1, 1, 0, 2 }
        };

        int[] expected = { 2, 1 };
        int[] result = BestDayToCrossCode.BestDayToCross(altitudes, forecasts);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestNoCrossingPossible()
    {
        int[] altitudes = { 0, 2, 4, 6 };
        int[][] forecasts = {
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 }
        };

        int[] expected = { -1, -1 };
        int[] result = BestDayToCrossCode.BestDayToCross(altitudes, forecasts);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestImmediateCrossing()
    {
        int[] altitudes = { 0, 1, 1, 0 };
        int[][] forecasts = {
            new int[] { 0, 0, 0, 0 }
        };

        int[] expected = { 0, 2 };
        int[] result = BestDayToCrossCode.BestDayToCross(altitudes, forecasts);
        Assert.Equal(expected, result);
    }
}