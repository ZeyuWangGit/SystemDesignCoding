namespace Coding.CodeDesignTask.Voter;

public class VotingStrategyOne
{
    private Dictionary<string, (int score, int lastTimestamp)> candidates;
    private int currentTimestamp;

    public VotingStrategyOne()
    {
        candidates = new Dictionary<string, (int, int)>();
        currentTimestamp = 0;
    }

    public void Vote(List<List<string>> ballots)
    {
        foreach (var ballot in ballots)
        {
            currentTimestamp++;
            for (var i = 0; i < ballot.Count && i < 3; i++)
            {
                var points = 3 - i;
                var candidate = ballot[i];
                if (!candidates.ContainsKey(candidate))
                {
                    candidates.Add(candidate, (0, currentTimestamp));
                }

                candidates[candidate] = (candidates[candidate].score + points, currentTimestamp);
            }
        }
    }

    public List<string> GetResults()
    {
        return candidates.OrderByDescending(x => x.Value.score)
            .ThenBy(x => x.Value.lastTimestamp)
            .Select(x => x.Key)
            .ToList();
    }
}