namespace Coding.DSA.Voting;

public interface IVoting
{
    List<string> GetVotingResult(List<List<string>> ballots);
}