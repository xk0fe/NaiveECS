using NaiveECS.Core;
using NaiveECS.Example.Components;
using NaiveECS.Extensions;

namespace NaiveECS.Example.Systems;

public class InitializeHealthSystem : ISystem
{
    private Filter _filter;
    
    private const int DEFAULT_HEALTH = 100;
    
    public void Awake()
    {
        _filter = new Filter().With<CharacterComponent>().Without<HealthComponent>();
    }

    public void Update(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            var health = new HealthComponent
            {
                Value = DEFAULT_HEALTH
            };
            entity.SetComponent(ref health);
        }
    }
}