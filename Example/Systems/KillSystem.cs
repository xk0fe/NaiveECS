using NaiveECS.Core;
using NaiveECS.Example.Components;
using NaiveECS.Example.Constants;
using NaiveECS.Extensions;

namespace NaiveECS.Example.Systems;

public class KillSystem : ISystem
{
    private Filter _filter;
    private Filter _playerFilter;
    private Filter _gridFilter;

    public void Awake()
    {
        _filter = new Filter().With<PositionComponent, KillComponent>();
        _playerFilter = new Filter().With<PlayerComponent>();
        _gridFilter = new Filter().With<GridComponent>(); // time to inject Grid instead of doing it ECS way
    }

    public void Update(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            var positionComponent = entity.GetComponent<PositionComponent>();

            foreach (var gridEntity in _gridFilter)
            {
                var gridComponent = gridEntity.GetComponent<GridComponent>();
                gridComponent.Value.SetOccupied(positionComponent.X, positionComponent.Y, false);
            }

            foreach (var playerEntity in _playerFilter)
            {
                var playerComponent = playerEntity.GetComponent<PlayerComponent>();
                playerComponent.Experience += GameSettings.EXP_PER_KILL;
                if (playerComponent.Experience >= GameSettings.EXP_PER_LEVEL)
                {
                    playerComponent.Level++;
                    playerComponent.Experience = 0;
                }
            }
            
            entity.Remove();
        }
    }
}