using NaiveECS.Example.Roguelike.Common;
using NaiveECS.Extensions;

namespace NaiveECS.Example.Roguelike.Systems;

public class GridSystem : ISystem
{
    private Grid _grid;
    
    public GridSystem(Grid grid)
    {
        _grid = grid;
    }

    public void Awake()
    {
    }

    public void Update(float deltaTime)
    {
        _grid.DisplayGrid();
    }
}