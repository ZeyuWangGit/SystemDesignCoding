namespace Coding.CodeDesignTask.SnakeGame;

public class SnakeGameV1
{
    private readonly int[,] _map;
    private readonly LinkedList<(int x, int y)> _snake = new LinkedList<(int x, int y)>();
    private readonly Queue<(int x, int y)> _foods = new Queue<(int x, int y)>();
    private readonly int _width;
    private readonly int _height;
    private readonly Dictionary<string, int[]> _directions = new Dictionary<string, int[]>();
    public int Score { get; private set; } = 0;
    
    
    public SnakeGameV1(int width, int height, int[][] foods)
    {
        _width = width;
        _height = height;
        _map = new int[_height, _width];
        _map[0, 0] = 1;
        _snake.AddFirst((0, 0));
        foreach (var food in foods)
        {
            _foods.Enqueue((food[0], food[1]));
        }
        _directions.Add("U", [-1, 0]);
        _directions.Add("D", [1, 0]);
        _directions.Add("R", [0, 1]);
        _directions.Add("L", [0, -1]);
    }

    public int Move(string direction)
    {
        var currPos = _snake.First.Value;
        var newX = currPos.x + _directions[direction][0];
        var newY = currPos.y + _directions[direction][1];

        if (newX < 0 || newX >= _height || newY < 0 || newY >= _width)
        {
            Score = -1;
            return -1;
        }

        var tail = _snake.Last.Value;
        _map[tail.x, tail.y] = 0;
        _snake.RemoveLast();

        if (_map[newX, newY] == 1)
        {
            Score = -1;
            return -1;
        }
        
        _map[newX, newY] = 1;
        _snake.AddFirst((newX, newY));
        if (_foods.Count > 0 && _foods.Peek().x == newX && _foods.Peek().y == newY)
        {
            _foods.Dequeue();
            Score++;
            _snake.AddLast((tail.x, tail.y));
            _map[tail.x, tail.y] = 1;
        }

        return Score;
    }
}