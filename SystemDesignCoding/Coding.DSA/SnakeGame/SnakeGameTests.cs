namespace Coding.DSA.SnakeGame;

public class SnakeGameTests
{
    [Fact]
    public void SnakeGame_Test_Round1()
    {
        var snakeGame = new SnakeGame1(2, 3, [[1, 2], [0, 1]]);
         
        Assert.Equal(0 , snakeGame.Move("R")); // return 0
        Assert.Equal(0 , snakeGame.Move("D")); // return 0
        Assert.Equal(1 , snakeGame.Move("R")); // return 1, snake eats the first piece of food. The second piece of food appears at (0, 1).
        Assert.Equal(1 , snakeGame.Move("U")); // return 1
        Assert.Equal(2 , snakeGame.Move("L")); // return 2, snake eats the second food. No more food appears.
        Assert.Equal(-1, snakeGame.Move("U")); // return -1, game over because snake collides with border
    }
}