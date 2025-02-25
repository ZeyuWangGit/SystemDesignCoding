namespace Coding.CodeDesignTask.Voter;

public class VotingStrategyTwo
{
    private Dictionary<string, (int score, int[] ranks)> candidates;

    public VotingStrategyTwo()
    {
        candidates = new Dictionary<string, (int, int[])>();
    }

    public void Vote(List<List<string>> ballots)
    {
        foreach (var ballot in ballots)
        {
            for (var i = 0; i < ballot.Count && i < 3; i++)
            {
                var points = 3 - i;
                var candidate = ballot[i];
                if (!candidates.ContainsKey(candidate))
                {
                    candidates.Add(candidate, (0, new int[3]));
                }

                var rank = candidates[candidate].ranks;
                rank[i]++;
                candidates[candidate] = (candidates[candidate].score + points, rank);
            }
        }
    }

    public List<string> GetResults()
    {
        return candidates.OrderByDescending(x => x.Value.score)
            .ThenByDescending(x => x.Value.ranks[0])
            .ThenByDescending(x => x.Value.ranks[1])
            .ThenByDescending(x => x.Value.ranks[2])
            .Select(x => x.Key)
            .ToList();
    }
}