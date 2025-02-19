namespace Coding.Karat;

public static class NonogramValidatorCode
{
    /*
    A nonogram is a logic puzzle, similar to a crossword, in which the player is given a blank grid and has to color it according to some instructions.
    Specifically, each cell can be either black or white, which we will represent as 0 for black and 1 for white.

    +------------+
    | 1  1  1  1 |
    | 0  1  1  1 |
    | 0  1  0  0 |
    | 1  1  0  1 |
    | 0  0  1  1 |
    +------------+

    For each row and column, the instructions give the lengths of contiguous runs of black (0) cells.
    For example, the instructions for one row of [ 2, 1 ] indicate that there must be a run of two black cells, followed later by another run of one black cell, and the rest of the row filled with white cells.

    These are valid solutions: [ 1, 0, 0, 1, 0 ] and [ 0, 0, 1, 1, 0 ] and also [ 0, 0, 1, 0, 1 ]
    This is not valid: [ 1, 0, 1, 0, 0 ] since the runs are not in the correct order.
    This is not valid: [ 1, 0, 0, 0, 1 ] since the two runs of 0s are not separated by 1s.

    Your job is to write a function to validate a possible solution against a set of instructions. Given a 2D matrix representing a player's solution; and instructions for each row along with additional instructions for each column; return True or False according to whether both sets of instructions match.

    Example instructions #1

    matrix1 = [[1,1,1,1],
               [0,1,1,1],
               [0,1,0,0],
               [1,1,0,1],
               [0,0,1,1]]
    rows1_1    =  [], [1], [1,2], [1], [2]
    columns1_1 =  [2,1], [1], [2], [1]
    validateNonogram(matrix1, rows1_1, columns1_1) => True

    Example solution matrix:
    matrix1 ->
                                   row
                +------------+     instructions
                | 1  1  1  1 | <-- []
                | 0  1  1  1 | <-- [1]
                | 0  1  0  0 | <-- [1,2]
                | 1  1  0  1 | <-- [1]
                | 0  0  1  1 | <-- [2]
                +------------+
                  ^  ^  ^  ^
                  |  |  |  |
    column       [2,1] | [2] |
    instructions      [1]   [1]


    Example instructions #2

    (same matrix as above)
    rows1_2    =  [], [], [1], [1], [1,1]
    columns1_2 =  [2], [1], [2], [1]
    validateNonogram(matrix1, rows1_2, columns1_2) => False

    The second and third rows and the first column do not match their respective instructions.

    Example instructions #3

    matrix2 = [
    [ 1, 1 ],
    [ 0, 0 ],
    [ 0, 0 ],
    [ 1, 0 ]
    ]
    rows2_1    = [], [2], [2], [1]
    columns2_1 = [1, 1], [3]
    validateNonogram(matrix2, rows2_1, columns2_1) => False

    The black cells in the first column are not separated by white cells.

    n: number of rows in the matrix
    m: number of columns in the matrix
     */
    public static bool IsValidNonogram(int[][] matrix, List<List<int>> rowConstraints, List<List<int>> colConstraints)
    {
        var isRowValid = true;
        var isColValid = true;
        var col = matrix.Length;
        var row = matrix[0].Length;

        for (var i = 0; i < col; i++)
        {
            if (!IsValid(matrix[i].ToList(), rowConstraints[i]))
            {
                isRowValid = false;
            }
        }

        for (var j = 0; j < row; j++)
        {
            var data = new List<int>();
            for (var i = 0; i < col; i++)
            {
                data.Add(matrix[i][j]);
            }
            if (!IsValid(data, colConstraints[j]))
            {
                isColValid = false;
            }
        }

        return isRowValid && isColValid;
    }

    public static bool IsValid(List<int> data, List<int> constraints)
    {
        var res = new List<int>();
        var count = 0;
        foreach (var t in data)
        {
            if (t == 0)
            {
                count++;
            } else if(t == 1 && count > 0)
            {
                res.Add(count);
                count = 0;
            }
        }

        if (count > 0)
        {
            res.Add(count);
        }

        return string.Join("-", res.ToArray()) == string.Join("-", constraints.ToArray());
    }
}