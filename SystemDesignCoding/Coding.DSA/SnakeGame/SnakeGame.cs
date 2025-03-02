namespace Coding.DSA.SnakeGame;

public class SnakeGame: ISnakeGame
{
    private readonly int[,] _map;
    private readonly LinkedList<(int x, int y)> _snake = [];
    private readonly Queue<(int x, int y)> _foods;
    private readonly int _height;
    private readonly int _width;
    private readonly Dictionary<string, int[]> _directions;
    private int _score = 0;
    
    public SnakeGame(int width, int height, int[][] foods)
    { 
        _height = height;
        _width = width;
        _map = new int[_height, _width];
        _foods = new Queue<(int x, int y)>();
        _directions = new Dictionary<string, int[]>();
        _map[0, 0] = 1;
        _snake.AddFirst((0, 0));

        foreach (var food in foods)
        {
            _foods.Enqueue((food[0], food[1]));
        }
        
        _directions.Add("up", [-1, 0]);
        _directions.Add("down", [1, 0]);
        _directions.Add("left", [0, -1]);
        _directions.Add("right", [0, 1]);
        
    }
    public int Move(string dir)
    {
        if (_snake.Count == 0 || _snake.First == null)
        {
            return -1;
        }

        var (currX, currY) = _snake.First.Value;
        var newX = currX + _directions[dir][0];
        var newY = currY + _directions[dir][1];

        if (newX < 0 || newX >= _height || newY < 0 || newY >= _width)
        {
            return -1;
        }

        var (tailX, tailY) = _snake.Last();
        _map[tailX, tailY] = 0;
        _snake.RemoveLast();

        if (_map[newX, newY] == 1)
        {
            return -1;
        }
        
        _map[newX, newY] = 1;
        _snake.AddFirst((newX, newY));
        if (_foods.Count > 0 && _foods.Peek().x == newX && _foods.Peek().y == newY)
        {
            _score++;
            _foods.Dequeue();
            _map[tailX, tailY] = 1;
            _snake.AddLast((tailX, tailY));
        }

        return _score;
    }
}