namespace Coding.DSA.Voting;

public class VotingStrategyTests
{
    [Fact]
    public void VotingStrategy_ShouldHandle_NonTieConsition()
    {
        var voting1 = new VotingStrategyOne();
        var voting2 = new VotingStrategyTwo();

        List<List<string>> ballots = new List<List<string>>()
        {
            new List<string> { "Alice", "Bob", "Charlie" }, // Alice +3, Bob +2, Charlie +1
            new List<string> { "Alice", "Bob", "Charlie" }, // Alice +3, Bob +2, Charlie +1
            new List<string> { "Bob", "Charlie", "Alice" }, // Bob +3, Charlie +2, Alice +1
            new List<string> { "Charlie", "Alice", "Bob" }  // Charlie +3, Alice +2, Bob +1
        };
        
        var result1 = voting1.GetVotingResult(ballots);
        var result2 = voting2.GetVotingResult(ballots);
        
        List<string> expected = new List<string> { "Alice", "Bob", "Charlie" };
        Assert.Equal(expected, result1);
        Assert.Equal(expected, result2);
    }
}