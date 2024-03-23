using NaiveECS.Core;
using NaiveECS.Example.Components;
using NaiveECS.Extensions;

namespace NaiveECS.Example.Systems;

public class InitializeWarriorsSystem : ISystem
{
    private Filter _filter;

    private const int WARRIOR_COUNT = 1;

    public void Awake()
    {
        _filter = new Filter().With<PositionComponent>();
        var world = World.Default();
        for (var i = 0; i < WARRIOR_COUNT; i++)
        {
            var entity = world.CreateEntity();
            var position = new PositionComponent
            {
                X = 0,
                Y = 0,
            };
            entity.SetComponent(ref position);
        }
    }

    public void Update(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            entity.TryGetComponent(out PositionComponent component);
            // ref var component = ref entity.GetComponentRef<PositionComponent>();
            component.X += 1;
            if (component.X > 909090)
            {
                component.X *= -1;
            }
            
            entity.SetComponent(ref component);
            Console.WriteLine("Warrior position: " + component.X);
        }
    }

    public void Dispose()
    {
    }
}