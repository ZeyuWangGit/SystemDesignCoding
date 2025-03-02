namespace Coding.DSA.Voting;

// Tire breaking Strategy: Who reach the same point first who won
// First 3 points, Second 2 points, last 1 point

public class VotingStrategyOne: IVoting
{
    private readonly Dictionary<string, (int score, int timestamp)> _votes = new Dictionary<string, (int, int)>();
    private int _currentTimestamp = 0;
    
    public List<string> GetVotingResult(List<List<string>> ballots)
    {
        foreach (var ballot in ballots)
        {
            _currentTimestamp++;
            for (var i = 0; i < 3; i++)
            {
                var score = 3 - i;
                var candidate = ballot[i];
                if (!_votes.ContainsKey(candidate))
                {
                    _votes.Add(candidate, (0, _currentTimestamp));
                }
                _votes[candidate] = (_votes[candidate].score + score, _currentTimestamp);
                
            }
        }

        return _votes.OrderByDescending(x => x.Value.score)
            .ThenBy(x => x.Value.timestamp)
            .Select(x => x.Key)
            .ToList();
    }
}