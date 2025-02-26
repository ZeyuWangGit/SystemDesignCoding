namespace Coding.CodeDesignTask.SnakeGame;

public class SnakeGameV1Tests
{
    [Fact]
    public void SnakeGame_Test_Round1()
    {
        var snakeGame = new SnakeGameV1(3, 2, [[1, 2], [0, 1]]);
        snakeGame.Move("R"); // return 0
        Assert.Equal(0 , snakeGame.Score);
        snakeGame.Move("D"); // return 0
        Assert.Equal(0 , snakeGame.Score);
        snakeGame.Move("R"); // return 1, snake eats the first piece of food. The second piece of food appears at (0, 1).
        Assert.Equal(1 , snakeGame.Score);
        snakeGame.Move("U"); // return 1
        Assert.Equal(1 , snakeGame.Score);
        snakeGame.Move("L"); // return 2, snake eats the second food. No more food appears.
        Assert.Equal(2 , snakeGame.Score);
        snakeGame.Move("U"); // return -1, game over because snake collides with border
        Assert.Equal(-1, snakeGame.Score);
    }
}