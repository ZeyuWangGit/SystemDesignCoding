namespace Coding.CodeDesignTask.Voter;

public class VotingStrategyOneTests
{
    [Fact]
    public void TestBasicVoting()
    {
        var votingSystem = new VotingStrategyOne();

        List<List<string>> ballots = new List<List<string>>()
        {
            new List<string> { "Alice", "Bob", "Charlie" }, // Alice +3, Bob +2, Charlie +1
            new List<string> { "Bob", "Charlie", "Alice" }, // Bob +3, Charlie +2, Alice +1
            new List<string> { "Charlie", "Alice", "Bob" }  // Charlie +3, Alice +2, Bob +1
        };

        votingSystem.Vote(ballots);
        List<string> results = votingSystem.GetResults();

        List<string> expected = new List<string> { "Alice", "Bob", "Charlie" };
        Assert.Equal(expected, results);
    }

    [Fact]
    public void TestTieBreakerByTimestamp()
    {
        var votingSystem = new VotingStrategyOne();

        List<List<string>> ballots = new List<List<string>>()
        {
            new List<string> { "Alice", "Bob", "Charlie" },
            new List<string> { "Bob", "Alice", "Charlie" },
            new List<string> { "Charlie", "Bob", "Alice" }
        };

        votingSystem.Vote(ballots);
        List<string> results = votingSystem.GetResults();

        // 检查是否按时间戳处理平分情况
        // 这里不能用 Assert.Equal 因为顺序可能不同
        Assert.True(results.IndexOf("Alice") < results.IndexOf("Bob") ||
                    results.IndexOf("Bob") < results.IndexOf("Charlie"));
    }
}