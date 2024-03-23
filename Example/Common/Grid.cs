using NaiveECS.Example.Constants;

namespace NaiveECS.Example.Common;

public class Grid
{
    private bool[,] _grid;
    private int _width;
    private int _height;

    public Grid(int width, int height)
    {
        this._width = width;
        this._height = height;
        _grid = new bool[width, height];
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                _grid[x, y] = true;
            }
        }
    }

    public void SetObstacle(int x, int y)
    {
        if (IsValidPosition(x, y))
        {
            _grid[x, y] = false;
        }
    }

    public bool CanMoveTo(int x, int y)
    {
        return IsValidPosition(x, y) && _grid[x, y];
    }

    private bool IsValidPosition(int x, int y)
    {
        return x >= 0 && x < _width && y >= 0 && y < _height;
    }

    public void DisplayGrid()
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                var isObstacle = !_grid[x, y];
                Console.ForegroundColor = isObstacle ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write(isObstacle ? GameSettings.WALL_SYMBOL : GameSettings.FLOOR_SYMBOL);
            }

            Console.WriteLine();
        }
    }
}