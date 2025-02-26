namespace Coding.CodeDesignTask.Voter;

public class VotingStrategyTwoTests
{
    [Fact]
    public void TestTieBreakerByRankCount()
    {
        var votingSystem = new VotingStrategyTwo();

        List<List<string>> ballots = new List<List<string>>()
        {
            new List<string> { "Alice", "Bob", "Charlie" }, // Alice +3, Bob +2, Charlie +1
            new List<string> { "Bob", "Alice", "Charlie" }, // Bob +3, Alice +2, Charlie +1
            new List<string> { "Charlie", "Alice", "Bob" }, // Charlie +3, Alice +2, Bob +1
            new List<string> { "Charlie", "Alice", "Bob" }  // Charlie +3, Alice +2, Bob +1
        };

        votingSystem.Vote(ballots);
        List<string> results = votingSystem.GetResults();

        // Alice 和 Bob 得分相同，但 Alice 拿到的第一名票数较多，应排名更高
        List<string> expected = new List<string> { "Alice", "Charlie", "Bob" };
        Assert.Equal(expected, results);
    }

    [Fact]
    public void TestTieBreakerBySecondAndThirdRank()
    {
        var votingSystem = new VotingStrategyTwo();

        List<List<string>> ballots = new List<List<string>>()
        {
            new List<string> { "Alice", "Bob", "Charlie" },  // Alice +3, Bob +2, Charlie +1
            new List<string> { "Alice", "Charlie", "Bob" },  // Alice +3, Charlie +2, Bob +1
            new List<string> { "Bob", "Alice", "Charlie" },  // Bob +3, Alice +2, Charlie +1
            new List<string> { "Bob", "Charlie", "Alice" },  // Bob +3, Charlie +2, Alice +1
            new List<string> { "Charlie", "Bob", "Alice" }   // Charlie +3, Bob +2, Alice +1
        };

        votingSystem.Vote(ballots);
        List<string> results = votingSystem.GetResults();

        // Alice 和 Bob 得分相同，第一名票数也相同，但 Bob 的第二名票数较多，应排名更高
        Assert.True(results.IndexOf("Bob") < results.IndexOf("Alice"));
    }
}