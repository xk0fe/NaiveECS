using NaiveECS.Core;
using NaiveECS.Example.Components;
using NaiveECS.Extensions;

namespace NaiveECS.Example.Systems;

public class NPCMovementSystem : ISystem
{
    private Filter _filter;
    private Filter _gridFilter;
    
    private Random _random;

    public void Awake()
    {
        _filter = new Filter().With<PositionComponent, CharacterComponent, DecisionDelayComponent>().Without<PlayerComponent>();
        _gridFilter = new Filter().With<GridComponent>();
        _random = new Random();
    }
    
    public void Update(float deltaTime)
    {
        foreach (var gridEntity in _gridFilter)
        {
            gridEntity.TryGetComponent(out GridComponent gridComponent);
            var grid = gridComponent.Value;
            
            foreach (var entity in _filter)
            {
                entity.TryGetComponent(out DecisionDelayComponent decisionDelayComponent);
                decisionDelayComponent.CurrentDelay += deltaTime;
                if (decisionDelayComponent.CurrentDelay < decisionDelayComponent.Delay)
                {
                    entity.SetComponent(ref decisionDelayComponent);
                    continue;
                }

                decisionDelayComponent.CurrentDelay = 0;
                entity.SetComponent(ref decisionDelayComponent);
                
                entity.TryGetComponent(out PositionComponent position);
                var previousX = position.X;
                var previousY = position.Y;
                position.X += _random.Next(-1, 2);
                position.Y += _random.Next(-1, 2);
                if (!grid.CanMoveTo(position.X, position.Y, out var reason))
                {
                    continue;
                }
                
                grid.SetOccupied(previousX, previousY, false);
                grid.SetOccupied(position.X, position.Y, true);
                
                entity.SetComponent(ref position);
            }
        }
    }
}