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
            var gridComponent = gridEntity.GetComponent<GridComponent>();
            var grid = gridComponent.Value;
            
            foreach (var entity in _filter)
            {
                var decisionDelayComponent = entity.GetComponent<DecisionDelayComponent>();
                decisionDelayComponent.CurrentDelay += deltaTime;
                if (decisionDelayComponent.CurrentDelay < decisionDelayComponent.Delay)
                {
                    continue;
                }

                decisionDelayComponent.CurrentDelay = 0;
                
                var position = entity.GetComponent<PositionComponent>();
                var previousX = position.X;
                var previousY = position.Y;
                var randomX = _random.Next(-1, 2);
                var randomY = _random.Next(-1, 2);
                if (!grid.CanMoveTo(position.X + randomX, position.Y + randomY, out var reason))
                {
                    continue;
                }

                position.X += randomX;
                position.Y += randomY;
                
                grid.SetOccupied(previousX, previousY, false);
                grid.SetOccupied(position.X, position.Y, true);
            }
        }
    }
}