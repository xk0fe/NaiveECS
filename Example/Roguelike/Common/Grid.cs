using NaiveECS.Example.Roguelike.Constants;

namespace NaiveECS.Example.Roguelike.Common;

public class Grid
{
    private bool[,] _cells;
    private bool[,] _characterCells;
    private int _width;
    private int _height;

    public Grid(int width, int height)
    {
        _width = width;
        _height = height;
        InitializeGrid(ref _cells);
        InitializeGrid(ref _characterCells);
    }

    private void InitializeGrid(ref bool[,] grid)
    {
        grid = new bool[_width, _height];
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                grid[x, y] = true;
            }
        }
    }

    public void SetOccupied(int x, int y, bool isOccupied)
    {
        if (IsValidPosition(x, y))
        {
            _characterCells[x, y] = !isOccupied;
        }
    }
    
    public void SetObstacle(int x, int y)
    {
        if (IsValidPosition(x, y))
        {
            _cells[x, y] = false;
        }
    }

    public bool CanMoveTo(int x, int y, out CellBlockedReason blockedReason)
    {
        blockedReason = CellBlockedReason.None;
        if (!IsValidPosition(x, y))
        {
            blockedReason = CellBlockedReason.OutOfBounds;
            return false;
        }

        if (!_cells[x, y])
        {
            blockedReason = CellBlockedReason.Blocked;
            return false;
        }
        
        if (!_characterCells[x, y])
        {
            blockedReason = CellBlockedReason.Occupied;
            return false;
        }

        return true;
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
                var isObstacle = !_cells[x, y];
                Console.ForegroundColor = isObstacle ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write(isObstacle ? GameSettings.WALL_SYMBOL : GameSettings.FLOOR_SYMBOL);
            }

            Console.WriteLine();
        }
    }
}