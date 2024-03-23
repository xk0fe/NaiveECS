using NaiveECS.Core;
using NaiveECS.Example.Components;
using NaiveECS.Extensions;

namespace NaiveECS.Example.Systems;

public class InitializeWarriorsSystem : ISystem
{
    private Filter _filter;

    private const int WARRIOR_COUNT = 50;

    public void Awake()
    {
        var world = World.Default();
        for (var i = 0; i < WARRIOR_COUNT; i++)
        {
            var entity = world.CreateEntity();
            var position = new PositionComponent();
            entity.SetComponent(ref position);
        }
    }

    public void Update(float deltaTime)
    {
    }

    public void Dispose()
    {
    }
}