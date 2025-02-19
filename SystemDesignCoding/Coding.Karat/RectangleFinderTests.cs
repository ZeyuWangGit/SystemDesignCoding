namespace Coding.Karat;

using Xunit;

public class RectangleFinderTests
{
    [Fact]
    public void Test_FindOneRectangle_ExampleCase()
    {
        int[][] board = {
            new int[] {1, 1, 1, 1},
            new int[] {1, 0, 0, 1},
            new int[] {1, 0, 0, 1},
            new int[] {1, 1, 1, 1}
        };

        int[][] expected = new int[][] {
            new int[] {1, 1}, // Top-left corner
            new int[] {2, 2}  // Bottom-right corner
        };

        var result = RectangleFinderCode.FindOneRectangle(board);

        Assert.NotNull(result);
        Assert.Equal(expected.Length, result.Count );
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], result[i]);
        }
    }

    [Fact]
    public void Test_FindOneRectangle_NoRectangle()
    {
        int[][] board = {
            new int[] {1, 1, 1},
            new int[] {1, 1, 1},
            new int[] {1, 1, 1}
        };

        int[][] expected = new int[0][]; // No rectangle found
        var result = RectangleFinderCode.FindOneRectangle(board);

        Assert.Empty(result);
    }

    [Fact]
    public void Test_FindOneRectangle_FullMatrixRectangle()
    {
        int[][] board = {
            new int[] {0, 0},
            new int[] {0, 0}
        };

        int[][] expected = new int[][] {
            new int[] {0, 0}, // Top-left corner
            new int[] {1, 1}  // Bottom-right corner
        };

        var result = RectangleFinderCode.FindOneRectangle(board);

        Assert.NotNull(result);
        Assert.Equal(expected.Length, result.Count);
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], result[i]);
        }
    }

    [Fact]
    public void Test_FindOneRectangle_SingleZero()
    {
        int[][] board = {
            new int[] {1, 1, 1},
            new int[] {1, 0, 1},
            new int[] {1, 1, 1}
        };

        int[][] expected = new int[][] {
            new int[] {1, 1}, // Single zero at (1,1)
            new int[] {1, 1}  // Same point, as it's a single cell
        };

        var result = RectangleFinderCode.FindOneRectangle(board);

        Assert.NotNull(result);
        Assert.Equal(expected.Length, result.Count);
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], result[i]);
        }
    }

    [Fact]
    public void Test_FindOneRectangle_LargeMatrix()
    {
        int[][] board = {
            new int[] {1, 1, 1, 1, 1},
            new int[] {1, 1, 1, 1, 1},
            new int[] {1, 1, 0, 0, 1},
            new int[] {1, 1, 0, 0, 1},
            new int[] {1, 1, 1, 1, 1}
        };

        int[][] expected = new int[][] {
            new int[] {2, 2}, // Top-left corner
            new int[] {3, 3}  // Bottom-right corner
        };

        var result = RectangleFinderCode.FindOneRectangle(board);

        Assert.NotNull(result);
        Assert.Equal(expected.Length, result.Count);
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], result[i]);
        }
    }
    
    [Fact]
        public void Test_FindMultipleRectangles_ExampleCase()
        {
            int[][] board = {
                new int[] {1, 1, 1, 1, 1},
                new int[] {1, 0, 0, 1, 1},
                new int[] {1, 0, 0, 1, 1},
                new int[] {1, 1, 1, 1, 1},
                new int[] {1, 0, 1, 1, 1},
                new int[] {1, 0, 1, 1, 1}
            };

            List<int[][]> expected = new List<int[][]>
            {
                new int[][] { new int[] {1, 1}, new int[] {2, 2} }, // First rectangle (1,1) to (2,2)
                new int[][] { new int[] {4, 1}, new int[] {5, 1} }  // Second rectangle (4,1) to (5,1)
            };

            var result = RectangleFinderCode.FindMultipleRectangles(board);

            Assert.Equal(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i][0], result[i][0]); // Top-left corner
                Assert.Equal(expected[i][1], result[i][1]); // Bottom-right corner
            }
        }

        [Fact]
        public void Test_FindMultipleRectangles_NoRectangle()
        {
            int[][] board = {
                new int[] {1, 1, 1},
                new int[] {1, 1, 1},
                new int[] {1, 1, 1}
            };

            List<int[][]> expected = new List<int[][]>(); // No rectangles found
            var result = RectangleFinderCode.FindMultipleRectangles(board);

            Assert.Empty(result);
        }

        [Fact]
        public void Test_FindMultipleRectangles_SingleRectangle()
        {
            int[][] board = {
                new int[] {1, 1, 1},
                new int[] {1, 0, 1},
                new int[] {1, 1, 1}
            };

            List<int[][]> expected = new List<int[][]>
            {
                new int[][] { new int[] {1, 1}, new int[] {1, 1} } // Single 0 at (1,1)
            };

            var result = RectangleFinderCode.FindMultipleRectangles(board);

            Assert.Equal(expected.Count, result.Count);
            Assert.Equal(expected[0][0], result[0][0]); // Top-left corner
            Assert.Equal(expected[0][1], result[0][1]); // Bottom-right corner
        }

        [Fact]
        public void Test_FindMultipleRectangles_LargeCase()
        {
            int[][] board = {
                new int[] {1, 1, 1, 1, 1, 1, 1},
                new int[] {1, 0, 0, 1, 0, 0, 1},
                new int[] {1, 0, 0, 1, 0, 0, 1},
                new int[] {1, 1, 1, 1, 1, 1, 1},
                new int[] {1, 0, 0, 0, 0, 1, 1},
                new int[] {1, 0, 0, 0, 0, 1, 1}
            };

            List<int[][]> expected = new List<int[][]>
            {
                new int[][] { new int[] {1, 1}, new int[] {2, 2} }, // First rectangle (1,1) to (2,2)
                new int[][] { new int[] {1, 4}, new int[] {2, 5} }, // Second rectangle (1,4) to (2,5)
                new int[][] { new int[] {4, 1}, new int[] {5, 4} }  // Third rectangle (4,1) to (5,4)
            };

            var result = RectangleFinderCode.FindMultipleRectangles(board);

            Assert.Equal(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i][0], result[i][0]); // Top-left corner
                Assert.Equal(expected[i][1], result[i][1]); // Bottom-right corner
            }
        }
        
        [Fact]
        public void Test_FindMultipleShapes_ExampleCase()
        {
            int[][] board = {
                new int[] {1, 1, 1, 1, 1},
                new int[] {1, 0, 0, 1, 1},
                new int[] {1, 0, 0, 1, 1},
                new int[] {1, 1, 1, 1, 1},
                new int[] {1, 0, 1, 1, 1},
                new int[] {1, 0, 1, 1, 1}
            };

            List<List<int[]>> expected = new List<List<int[]>>
            {
                new List<int[]> // First shape
                {
                    new int[] {1, 1}, new int[] {2, 1},new int[] {2, 2},new int[] {1, 2},
                     
                },
                new List<int[]> // Second shape
                {
                    new int[] {4, 1}, new int[] {5, 1}
                }
            };

            var result = RectangleFinderCode.FindMultipleShapes(board);

            Assert.Equal(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Count, result[i].Count);
                for (int j = 0; j < expected[i].Count; j++)
                {
                    Assert.Equal(expected[i][j], result[i][j]);
                }
            }
        }

        [Fact]
        public void Test_FindMultipleShapes_NoShapes()
        {
            int[][] board = {
                new int[] {1, 1, 1},
                new int[] {1, 1, 1},
                new int[] {1, 1, 1}
            };

            List<List<int[]>> expected = new List<List<int[]>>(); // No shapes found
            var result = RectangleFinderCode.FindMultipleShapes(board);

            Assert.Empty(result);
        }

        [Fact]
        public void Test_FindMultipleShapes_SingleShape()
        {
            int[][] board = {
                new int[] {1, 1, 1},
                new int[] {1, 0, 1},
                new int[] {1, 1, 1}
            };

            List<List<int[]>> expected = new List<List<int[]>>
            {
                new List<int[]> { new int[] {1, 1} } // Single 0 at (1,1)
            };

            var result = RectangleFinderCode.FindMultipleShapes(board);

            Assert.Equal(expected.Count, result.Count);
            Assert.Equal(expected[0].Count, result[0].Count);
            Assert.Equal(expected[0][0], result[0][0]);
        }

        [Fact]
        public void Test_FindMultipleShapes_LargeCase()
        {
            int[][] board = {
                new int[] {1, 1, 1, 1, 1, 1, 1},
                new int[] {1, 0, 0, 1, 0, 0, 1},
                new int[] {1, 0, 0, 1, 0, 0, 1},
                new int[] {1, 1, 1, 1, 1, 1, 1},
                new int[] {1, 0, 0, 0, 0, 1, 1},
                new int[] {1, 0, 0, 0, 0, 1, 1}
            };

            List<List<int[]>> expected = new List<List<int[]>>
            {
                new List<int[]> // First shape
                {
                    new int[] {1, 1}, new int[] {2, 1},new int[] {2, 2},new int[] {1, 2},
                },
                new List<int[]> // Second shape
                {
                    new int[] {1, 4}, new int[] {2, 4},new int[] {2, 5}, new int[] {1, 5},
                     
                },
                new List<int[]> // Third shape
                {
                     new int[] { 4, 1 }, new int[] { 5, 1 }, new int[] { 5, 2 }, new int[] { 4, 2 }, new int[] { 4, 3 },
                     new int[] { 5, 3 }, new int[] { 5, 4 }, new int[] { 4, 4 },
                       
                }
            };

            var result = RectangleFinderCode.FindMultipleShapes(board);

            Assert.Equal(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Count, result[i].Count);
                for (int j = 0; j < expected[i].Count; j++)
                {
                    Assert.Equal(expected[i][j], result[i][j]);
                }
            }
        }
}
