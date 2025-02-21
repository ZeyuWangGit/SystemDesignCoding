namespace Coding.Karat;

public static class FriendsRelationshipCode
{
    /*
     * 问题 1：构建好友列表
     * 公司有一群员工，某些员工之间是朋友，朋友关系是 双向的（如果 A 是 B 的朋友，则 B 也是 A 的朋友）。
     * 给定一组 (employee1, employee2) 形式的好友关系，构建并返回所有员工的 好友列表。
     */
    public static Dictionary<int, HashSet<int>> BuildFriendshipMap(List<(int, int)> friendships)
    {
        var graph = new Dictionary<int, HashSet<int>>();
        foreach (var friendship in friendships)
        {
            var from = friendship.Item1;
            var to = friendship.Item2;
            if (!graph.ContainsKey(from))
            {
                graph.Add(from, new HashSet<int>());
            }

            if (!graph.ContainsKey(to))
            {
                graph.Add(to, new HashSet<int>());
            }

            graph[from].Add(to);
            graph[to].Add(from);
        }

        return graph;
    }
    
    /*
     * 问题 2：统计跨部门好友数量
     * 每个员工属于一个部门（Department），如果 两个朋友来自不同的部门，则称他们是跨部门朋友。
     * 给定一组 (employee1, employee2) 形式的好友关系，以及每个员工的部门信息，返回每个部门有多少人至少有一个跨部门好友。
     */
    public static Dictionary<string, int> CountCrossDepartmentFriends(
        List<(int, int)> friendships,
        Dictionary<int, string> departments)
    {
        var memberHasCrossDepartmentFriendsMap = new Dictionary<int, bool>();
        var relationshipGraph = BuildFriendshipMap(friendships);
        
        foreach (var employee in departments.Keys)
        {
            memberHasCrossDepartmentFriendsMap[employee] = false;
        }
        
        foreach (var (employee, friendsSet) in relationshipGraph)
        {
            var isEmployeeCrossDepartment = false;
            foreach (var friend in friendsSet)
            {
                if (departments[friend] != departments[employee])
                {
                    isEmployeeCrossDepartment = true;
                    break;
                }
            }

            memberHasCrossDepartmentFriendsMap[employee] = isEmployeeCrossDepartment;
        }

        var res = new Dictionary<string, int>();
        foreach (var (employee, department) in departments)
        {
            res.TryAdd(department, 0);

            if (memberHasCrossDepartmentFriendsMap[employee])
            {
                res[department]++;
            }
        }

        return res;
    }
    
    /*
     * 问题 3：判断是否所有员工在同一个社交圈
     * 如果所有员工的好友关系可以连接成一个 社交圈（Connected Component），则返回 true，否则返回 false。
     */
    public static bool IsSingleSocialCircle(List<(int, int)> friendships)
    {
        var friendMap = BuildFriendshipMap(friendships);
        var visited = new HashSet<int>();
        Dfs(friendMap, visited, friendships[0].Item1);
        return visited.Count == friendMap.Count;
    }

    private static void Dfs(Dictionary<int, HashSet<int>> graph, HashSet<int> visited, int employee)
    {
        if (!visited.Add(employee))
        {
            return;
        }

        foreach (var friend in graph[employee])
        {
            Dfs(graph, visited, friend);
        }
        
    }
}