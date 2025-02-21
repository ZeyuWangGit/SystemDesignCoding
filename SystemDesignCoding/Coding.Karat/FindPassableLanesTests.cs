namespace Coding.Karat;

public class FindPassableLanesTests
{
    [Fact]
        public void Test_Board1()
        {
            char[][] board1 = {
                new char[] {'+', '+', '+', '0', '+', '0', '0'},
                new char[] {'0', '0', '+', '0', '0', '0', '0'},
                new char[] {'0', '0', '0', '0', '+', '0', '0'},
                new char[] {'+', '+', '+', '0', '0', '+', '0'},
                new char[] {'0', '0', '0', '0', '0', '0', '0'}
            };

            var result = FindPassableLanesCode.FindPassableLanes(board1);
            Assert.Equal("Rows: [4], Columns: [3,6]", result);
        }

        [Fact]
        public void Test_Board2()
        {
            char[][] board2 = {
                new char[] {'+', '+', '+', '0', '+', '0', '0'},
                new char[] {'0', '0', '0', '0', '0', '+', '0'},
                new char[] {'0', '0', '+', '0', '0', '0', '0'},
                new char[] {'0', '0', '0', '0', '+', '0', '0'},
                new char[] {'+', '+', '+', '0', '0', '0', '+'}
            };

            var result = FindPassableLanesCode.FindPassableLanes(board2);
            Assert.Equal("Rows: [], Columns: [3]", result);
        }

        [Fact]
        public void Test_Board3()
        {
            char[][] board3 = {
                new char[] {'+', '+', '+', '0', '+', '0', '0'},
                new char[] {'0', '0', '0', '0', '0', '0', '0'},
                new char[] {'0', '0', '+', '+', '0', '+', '0'},
                new char[] {'0', '0', '0', '0', '+', '0', '0'},
                new char[] {'+', '+', '+', '0', '0', '0', '+'}
            };

            var result = FindPassableLanesCode.FindPassableLanes(board3);
            Assert.Equal("Rows: [1], Columns: []", result);
        }

        [Fact]
        public void Test_Board4()
        {
            char[][] board4 = {
                new char[] {'+'}
            };

            var result = FindPassableLanesCode.FindPassableLanes(board4);
            Assert.Equal("Rows: [], Columns: []", result);
        }

        [Fact]
        public void Test_Board5()
        {
            char[][] board5 = {
                new char[] {'0'}
            };

            var result = FindPassableLanesCode.FindPassableLanes(board5);
            Assert.Equal("Rows: [0], Columns: [0]", result);
        }
}