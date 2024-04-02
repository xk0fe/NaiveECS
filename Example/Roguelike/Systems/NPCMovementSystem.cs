using NaiveECS.Core;
using NaiveECS.Example.Roguelike.Common;
using NaiveECS.Example.Roguelike.Components;
using NaiveECS.Extensions;

namespace NaiveECS.Example.Roguelike.Systems;

public class NPCMovementSystem : ISystem
{
    private Filter _filter;
    private Filter _gridFilter;
    
    private Random _random;
    private Grid _grid;
    
    public NPCMovementSystem(Grid grid)
    {
        _grid = grid;
    }

    public void Awake()
    {
        _filter = new Filter().With<PositionComponent, CharacterComponent, DecisionDelayComponent>().Without<PlayerComponent>();
        _random = new Random();
    }

    public void Update(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            entity.TryGetComponent(out DecisionDelayComponent decisionDelayComponent);
            decisionDelayComponent.CurrentDelay += deltaTime;
            if (decisionDelayComponent.CurrentDelay < decisionDelayComponent.Delay)
            {
                entity.SetComponent(decisionDelayComponent);
                continue;
            }

            decisionDelayComponent.CurrentDelay = 0;
            entity.SetComponent(decisionDelayComponent);

            entity.TryGetComponent(out PositionComponent position);
            var previousX = position.X;
            var previousY = position.Y;
            position.X += _random.Next(-1, 2);
            position.Y += _random.Next(-1, 2);
            if (!_grid.CanMoveTo(position.X, position.Y, out var reason))
            {
                continue;
            }

            _grid.SetOccupied(previousX, previousY, false);
            _grid.SetOccupied(position.X, position.Y, true);

            entity.SetComponent(position);
        }
    }
}