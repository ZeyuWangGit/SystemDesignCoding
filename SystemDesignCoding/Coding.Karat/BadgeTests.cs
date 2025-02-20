using Xunit;

namespace Coding.Karat;

public class BadgeTests
{
    [Fact]
    public void Test_InvalidBadgeRecords_ExampleCase()
    {
        string[][] badgeRecords =
        [
            ["Martha", "exit"],
            ["Paul", "enter"],
            ["Martha", "enter"],
            ["Martha", "exit"],
            ["Jennifer", "enter"],
            ["Paul", "enter"],
            ["Curtis", "enter"],
            ["Paul", "exit"],
            ["Martha", "enter"],
            ["Martha", "exit"],
            ["Jennifer", "exit"]
        ];

        List<List<string>> expected =
        [
            ["Paul", "Curtis"],
            ["Martha"]
        ];

        var result = BadgeCode.InvalidBadgeRecords(badgeRecords);

        Assert.Equal(expected[0], result[0]);
        Assert.Equal(expected[1], result[1]);
    }

    [Fact]
    public void Test_InvalidBadgeRecords_NoInvalidEntries()
    {
        var badgeRecords = new[]
        {
            ["Alice", "enter"],
            ["Alice", "exit"],
            ["Bob", "enter"],
            new[] { "Bob", "exit" }
        };

        List<List<string>> expected =
        [
            [],
            []
        ];

        var result = BadgeCode.InvalidBadgeRecords(badgeRecords);

        Assert.Equal(expected[0], result[0]);
        Assert.Equal(expected[1], result[1]);
    }

    [Fact]
    public void Test_InvalidBadgeRecords_AllInvalidEntries()
    {
        var badgeRecords = new string[][]
        {
            ["Charlie", "exit"],
            ["Dana", "exit"],
            ["Eve", "enter"],
            ["Eve", "enter"]
        };

        List<List<string>> expected =
        [
            ["Eve"],
            ["Charlie", "Dana"]
        ];

        var result = BadgeCode.InvalidBadgeRecords(badgeRecords);

        Assert.Equal(expected[0], result[0]);
        Assert.Equal(expected[1], result[1]);
    }
    
    [Fact]
    public void Test_FindLargestGroup()
    {
        var records = new List<Record>
        {
            new Record { Name = "Curtis", Time = 2, ActionType = Action.Enter },
            new Record { Name = "John", Time = 1510, ActionType = Action.Exit },
            new Record { Name = "John", Time = 455, ActionType = Action.Enter },
            new Record { Name = "John", Time = 512, ActionType = Action.Exit },
            new Record { Name = "Jennifer", Time = 715, ActionType = Action.Exit },
            new Record { Name = "Steve", Time = 815, ActionType = Action.Enter },
            new Record { Name = "John", Time = 930, ActionType = Action.Enter },
            new Record { Name = "Steve", Time = 1000, ActionType = Action.Exit },
            new Record { Name = "Paul", Time = 1, ActionType = Action.Enter },
            new Record { Name = "Angela", Time = 1115, ActionType = Action.Enter },
            new Record { Name = "Curtis", Time = 1510, ActionType = Action.Exit },
            new Record { Name = "Angela", Time = 2045, ActionType = Action.Exit },
            new Record { Name = "Nick", Time = 630, ActionType = Action.Enter },
            new Record { Name = "Jennifer", Time = 30, ActionType = Action.Enter },
            new Record { Name = "Nick", Time = 30, ActionType = Action.Enter },
            new Record { Name = "Paul", Time = 2145, ActionType = Action.Exit },
            new Record { Name = "Ben", Time = 457, ActionType = Action.Enter },
            new Record { Name = "Ben", Time = 458, ActionType = Action.Exit },
            new Record { Name = "Robin", Time = 459, ActionType = Action.Enter },
            new Record { Name = "Robin", Time = 500, ActionType = Action.Exit },
        };

        string expected = "Paul, Curtis, Jennifer, Nick, John: 455 to 457, 458 to 459, 500 to 512";
        string result = BadgeCode.FindLargestGroup(records);

        Assert.Equal(expected, result);
    }
    // generate test case
    
    [Fact]
    public void Test_FindLargestGroup_AnotherCase()
    {
        var records = new List<Record>
        {
            new() { Name = "Paul", Time = 1545, ActionType = Action.Exit },
            new() { Name = "Curtis", Time = 1410, ActionType = Action.Enter },
            new() { Name = "Curtis", Time = 222, ActionType = Action.Enter },
            new() { Name = "Curtis", Time = 1630, ActionType = Action.Exit },
            new() { Name = "Paul", Time = 10, ActionType = Action.Enter },
            new() { Name = "Paul", Time = 1410, ActionType = Action.Enter },
            new() { Name = "John", Time = 330, ActionType = Action.Enter },
            new() { Name = "Jennifer", Time = 330, ActionType = Action.Enter },
            new() { Name = "Jennifer", Time = 1410, ActionType = Action.Exit },
            new() { Name = "John", Time = 1410, ActionType = Action.Exit },
            new() { Name = "Curtis", Time = 330, ActionType = Action.Exit },
            new() { Name = "Paul", Time = 330, ActionType = Action.Exit },
        };

        string expected = "Paul, Curtis: 222 to 330, 1410 to 1545";
        string result = BadgeCode.FindLargestGroup(records);

        Assert.Equal(expected, result);
    }
}