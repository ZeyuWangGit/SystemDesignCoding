namespace Coding.Karat;

public static class NodeFinderCode
{
    /*
    输入是int[][] input, input[0]是input[1] 的parent，比如 {{1,4}, {1,5}, {2,5}, {3,6}, {6,7}}会形成上面的图
    第一问是只有0个parents和只有1个parent的节点
     */
    public static List<int> FindNodesWithZeroOrOneParent(int[][] edges)
    {
        int row = edges.Length;
        var dict = new Dictionary<int, int>();

        for (int i = 0; i < row; i++)
        {
            int from = edges[i][0];
            int to = edges[i][1];
            if (!dict.ContainsKey(to))
            {
                dict.Add(to, 0);
            }
            if (!dict.ContainsKey(from))
            {
                dict.Add(from, 0);
            }

            dict[to]++;
        }

        var res = new List<int>();
        foreach (var pair in dict)
        {
            if (pair.Value <= 1)
            {
                res.Add(pair.Key);
            }
        }

        return res;
    }
    
    /*
    两个节点是否有公共祖先
     */
    public static bool HasCommonAncestor(int[][] edges, int x, int y)
    {
        var parents = GetParentSet(edges);
        var xAllAncestor = GetAllAncestor(x, parents);
        var yAllAncestor = GetAllAncestor(y, parents);
        xAllAncestor.IntersectWith(yAllAncestor);
        return xAllAncestor.Count > 0;
    }

    private static HashSet<int> GetAllAncestor(int x, Dictionary<int, HashSet<int>> parents)
    {
        var queue = new Queue<int>();
        var res = new HashSet<int>();
        queue.Enqueue(x);

        while (queue.Count > 0)
        {
            var size = queue.Count;
            for (var i = 0; i < size; i++)
            {
                var curr = queue.Dequeue();
                if (!parents.ContainsKey(curr))
                {
                    continue;
                }
                var parent = parents[curr];
                foreach (var item in parent.Where(item => res.Add(item)))
                {
                    queue.Enqueue(item);
                }
            }
        }

        return res;
    }

    private static Dictionary<int, HashSet<int>> GetParentSet(int[][] edge)
    {
        var directParents = new Dictionary<int, HashSet<int>>();
        foreach (var item in edge)
        {
            var from = item[0];
            var to = item[1];
            if (!directParents.ContainsKey(to))
            {
                directParents.Add(to, []);
            }

            directParents[to].Add(from);
        }

        return directParents;
    }
}