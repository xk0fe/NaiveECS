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
        _filter = new Filter().With<PositionComponent>().With<CharacterComponent>().Without<PlayerComponent>();
        _gridFilter = new Filter().With<GridComponent>();
        _random = new Random();
    }
    
    public void Update(float deltaTime)
    {
        foreach (var gridEntity in _gridFilter)
        {
            gridEntity.TryGetComponent(out GridComponent gridComponent);
            
            foreach (var entity in _filter)
            {
                entity.TryGetComponent(out PositionComponent component);
                component.X += _random.Next(-1, 2);
                component.Y += _random.Next(-1, 2);
                if (!gridComponent.Value.CanMoveTo(component.X, component.Y))
                {
                    continue;
                }
                
                entity.SetComponent(ref component);
            }
        }
    }

    public void Dispose()
    {
    }
}