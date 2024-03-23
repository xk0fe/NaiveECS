using NaiveECS.Core;
using NaiveECS.Example.Common;
using NaiveECS.Example.Components;
using NaiveECS.Example.Constants;
using NaiveECS.Extensions;

namespace NaiveECS.Example.Systems;

public class GridSystem : ISystem
{
    private Filter _filter;

    public void Awake()
    {
        _filter = new Filter().With<GridComponent>();
        CreateGrid();
    }

    private void CreateGrid()
    {
        var gridEntity = World.Default().CreateEntity();
        var grid = new Grid(GameSettings.MAP_WIDTH, GameSettings.MAP_HEIGHT);

        var random = new Random();
        var randomCount = random.Next(50, 150);
        while (randomCount > 0)
        {
            randomCount--;

            grid.SetObstacle(random.Next(0, 100), random.Next(0, 10));
        }

        var gridComponent = new GridComponent
        {
            Value = grid,
        };
        gridEntity.SetComponent(ref gridComponent);
    }

    public void Update(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            entity.TryGetComponent(out GridComponent grid);
            grid.Value.DisplayGrid();
        }
    }

    public void Dispose()
    {
    }
}