namespace Coding.DSA.Voting;

// Tire breaking Strategy: who has higher rank who won
// First 3 points, Second 2 points, last 1 point

public class VotingStrategyTwo: IVoting
{
    private readonly Dictionary<string, (int score, int[] rank)> _votes = new ();
    
    public List<string> GetVotingResult(List<List<string>> ballots)
    {
        foreach (var ballot in ballots)
        {
            for (var i = 0; i < 3; i++)
            {
                var score = 3 - i;
                var candidate = ballot[i];
                if (!_votes.ContainsKey(candidate))
                {
                    _votes.Add(candidate, (0, new int[3]));
                }

                _votes[candidate].rank[i]++;
                _votes[candidate] = (_votes[candidate].score + score, _votes[candidate].rank);
                
            }
        }

        return _votes.OrderByDescending(x => x.Value.score)
            .ThenByDescending(x => x.Value.rank[0])
            .ThenByDescending(x => x.Value.rank[1])
            .ThenByDescending(x => x.Value.rank[2])
            .Select(x => x.Key)
            .ToList();
    }
}