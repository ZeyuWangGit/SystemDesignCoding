namespace Coding.Karat;

public class ThrillingTeleportersTests
{
    [Fact]
    public void Test_Teleporters1_Case1()
    {
        var teleporters = new string[] { "3,1", "4,2", "5,10" };
        int dieSides = 6, start = 0, end = 20;
        var expected = new HashSet<int> { 1, 2, 10, 6 };

        var result = ThrillingTeleporters.Destinations(teleporters, dieSides, start, end);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_Teleporters2_Case1()
    {
        var teleporters = new string[] { "5,10", "6,22", "39,40", "40,49", "47,29" };
        int dieSides = 6, start = 46, end = 100;
        var expected = new HashSet<int> { 48, 49, 50, 51, 52, 29 };

        var result = ThrillingTeleporters.Destinations(teleporters, dieSides, start, end);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_Teleporters2_Case2()
    {
        var teleporters = new string[] { "5,10", "6,22", "39,40", "40,49", "47,29" };
        int dieSides = 10, start = 0, end = 50;
        var expected = new HashSet<int> { 1, 2, 3, 4, 7, 8, 9, 10, 22 };

        var result = ThrillingTeleporters.Destinations(teleporters, dieSides, start, end);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_Teleporters3_Case1()
    {
        var teleporters = new string[]
        {
            "6,18", "36,26", "41,21", "49,55", "54,52",
            "71,58", "74,77", "78,76", "80,73", "92,85"
        };
        int dieSides = 10, start = 95, end = 100;
        var expected = new HashSet<int> { 96, 97, 98, 99, 100 };

        var result = ThrillingTeleporters.Destinations(teleporters, dieSides, start, end);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_Teleporters3_Case2()
    {
        var teleporters = new string[]
        {
            "6,18", "36,26", "41,21", "49,55", "54,52",
            "71,58", "74,77", "78,76", "80,73", "92,85"
        };
        int dieSides = 10, start = 70, end = 100;
        var expected = new HashSet<int> { 72, 73, 75, 76, 77, 79, 58 };

        var result = ThrillingTeleporters.Destinations(teleporters, dieSides, start, end);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_Teleporters4_Case1()
    {
        var teleporters = new string[]
        {
            "97,93", "99,81", "36,33", "92,59", "17,3",
            "82,75", "4,1", "84,79", "54,4", "88,53",
            "91,37", "60,57", "61,7", "62,51", "31,19"
        };
        int dieSides = 6, start = 0, end = 100;
        var expected = new HashSet<int> { 1, 2, 3, 5, 6 };

        var result = ThrillingTeleporters.Destinations(teleporters, dieSides, start, end);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_Teleporters5_Case1()
    {
        var teleporters = new string[] { "3,8", "8,9", "9,3" };
        int dieSides = 6, start = 0, end = 20;
        var expected = new HashSet<int> { 1, 2, 4, 5, 6, 8 };

        var result = ThrillingTeleporters.Destinations(teleporters, dieSides, start, end);
        Assert.Equal(expected, result);
    }
}