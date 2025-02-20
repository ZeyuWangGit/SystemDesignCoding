namespace Coding.Karat;

public static class FindAllShortestPathsCode
{
    /*
    board3 中1代表钻石，给出起点和终点，问有没有一条不走回头路的路线，能从起点走到终点，并拿走所有的钻石，给出所有的最短路径。
    board3 = [
        [  1,  0,  0, 0, 0 ],
        [  0, -1, -1, 0, 0 ],
        [  0, -1,  0, 1, 0 ],
        [ -1,  0,  0, 0, 0 ],
        [  0,  1, -1, 0, 0 ],
        [  0,  0,  0, 0, 0 ],
    ]


    treasure(board3, (5, 0), (0, 4)) -> None

    treasure(board3, (5, 2), (2, 0)) ->
      [(5, 2), (5, 1), (4, 1), (3, 1), (3, 2), (2, 2), (2, 3), (1, 3), (0, 3), (0, 2), (0, 1), (0, 0), (1, 0), (2, 0)]
      Or
      [(5, 2), (5, 1), (4, 1), (3, 1), (3, 2), (3, 3), (2, 3), (1, 3), (0, 3), (0, 2), (0, 1), (0, 0), (1, 0), (2, 0)]

    treasure(board3, (0, 0), (4, 1)) ->
      [(0, 0), (0, 1), (0, 2), (0, 3), (1, 3), (2, 3), (2, 2), (3, 2), (3, 1), (4, 1)]
      Or
      [(0, 0), (0, 1), (0, 2), (0, 3), (1, 3), (2, 3), (3, 3), (3, 2), (3, 1), (4, 1)]
     */
    public static int minLen = int.MaxValue;

    public static List<List<(int, int)>> FindAllShortestPaths(int[][] board, (int, int) start, (int, int) end)
    {
        int m = board.Length;
        int n = board[0].Length;
        var path = new List<(int, int)>();
        var visited = new HashSet<(int, int)>();
        var res = new List<List<(int, int)>>();
        int diamondCount = 0;
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (board[i][j] == 1)
                {
                    diamondCount++;
                }
            }
        }

        DFS(board, start.Item1, start.Item2, end, path, diamondCount, visited, res);
        return res.Where(list => list.Count == minLen).ToList();
    }

    private static void DFS(int[][] board, int x, int y, (int, int) end, List<(int, int)> path, int remainDiamond,
        HashSet<(int, int)> visited, List<List<(int, int)>> res)
    {
        int m = board.Length;
        int n = board[0].Length;
        if (x < 0 || x >= m || y < 0 || y >= n || visited.Contains((x, y)) || board[x][y] == -1)
        {
            return;
        }

        path.Add((x, y));
        if (board[x][y] == 1)
        {
            remainDiamond--;
        }

        if (x == end.Item1 && y == end.Item2 && remainDiamond == 0)
        {
            if (path.Count < minLen)
            {
                minLen = path.Count;
                res.Clear();
                res.Add(new List<(int, int)>(path));
            }
            else if (path.Count == minLen)
            {
                res.Add(new List<(int, int)>(path));
            }
        }
        else
        {
            visited.Add((x, y));


            DFS(board, x + 1, y, end, path, remainDiamond, visited, res);
            DFS(board, x - 1, y, end, path, remainDiamond, visited, res);
            DFS(board, x, y + 1, end, path, remainDiamond, visited, res);
            DFS(board, x, y - 1, end, path, remainDiamond, visited, res);

            visited.Remove((x, y));
        }


        path.RemoveAt(path.Count - 1);
    }
}