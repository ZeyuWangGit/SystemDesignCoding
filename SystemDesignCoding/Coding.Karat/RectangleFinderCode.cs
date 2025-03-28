namespace Coding.Karat;

public static class RectangleFinderCode
{
    /*
     Q1:
        there is an image filled with 0s and 1s. There is at most one rectangle in this image filled with 0s, find the rectangle.
        Output could be the coordinates of top-left and bottom-right elements of the rectangle, or top-left element, width and height.
    */
    public static List<int[]> FindOneRectangle(int[][] board)
    {
        var res = new List<int[]>();
        if (board.Length == 0 || board[0].Length == 0)
        {
            return res;
        }

        var rows = board.Length;
        var cols = board[0].Length;

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                if (board[i][j] == 0)
                {
                    int width = 1;
                    while (j + width < cols && board[i][j + width] == 0)
                    {
                        width++;
                    }

                    int height = 1;
                    while (i + height < rows && board[i + height][j] == 0)
                    {
                        height++;
                    }
                    res.Add(new int[2]
                    {
                        i, j
                    });
                    res.Add(new int[2]
                    {
                        i + height - 1,
                        j + width - 1,
                    });
                    return res;
                }
            }
        }

        return res;
    }

    /*
     Q2
        for the same image, it is filled with 0s and 1s.
        It may have multiple rectangles filled with 0s. The rectangles are separated by 1s. Find all the rectangles.
    */
    public static List<int[][]> FindMultipleRectangles(int[][] board)
    {
        var res = new List<int[][]>();
        if (board.Length == 0 || board[0].Length == 0)
        {
            return res;
        }

        var rows = board.Length;
        var cols = board[0].Length;

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                if (board[i][j] == 0)
                {
                    var width = 1;
                    while (j + width < cols && board[i][j + width] == 0)
                    {
                        width++;
                    }

                    var height = 1;
                    while (i + height < rows && board[i + height][j] == 0)
                    {
                        height++;
                    }

                    for (var h = 0; h < height; h++)
                    {
                        for (var w = 0; w < width; w++)
                        {
                            board[i + h][j + w] = 1;
                        }
                    }
                    res.Add(new int[][]
                    {
                        [
                            i, j
                        ],
                        [
                            i + height - 1,
                            j + width - 1
                        ]
                    });
                }
            }
        }

        return res;
    }
    
    /*
     Q3
        the image has random shapes filled with 0s, separated by 1s. 
        Find all the shapes. Each shape is represented by coordinates of all the elements inside.
     */
    public static List<List<int[]>> FindMultipleShapes(int[][] board)
    {
        var res = new List<List<int[]>>();
        if (board.Length == 0 || board[0].Length == 0)
        {
            return res;
        }

        var rows = board.Length;
        var cols = board[0].Length;

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                if (board[i][j] == 0)
                {
                    var path = new List<int[]>();
                    FloodFillDfs(board, i, j, path);
                    res.Add(path);
                }
            }
        }

        return res;
    }

    private static void FloodFillDfs(int[][] board, int x, int y, List<int[]> path)
    {
        if (x < 0 || y < 0 || x >= board.Length || y >= board[0].Length || board[x][y] == 1)
        {
            return;
        }

        board[x][y] = 1;
        path.Add([
            x, y
        ]);
        FloodFillDfs(board, x + 1, y, path);
        FloodFillDfs(board, x - 1, y, path);
        FloodFillDfs(board, x, y + 1, path);
        FloodFillDfs(board, x, y - 1, path);
    }
}