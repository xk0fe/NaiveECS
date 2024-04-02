using NaiveECS.Core;
using NaiveECS.Example.Components;
using NaiveECS.Extensions;

namespace NaiveECS.Example.Systems;

public class DamageSystem : ISystem
{
    private Filter _requestFilter;
    private Filter _positionFilter;
    
    public void Awake()
    {
        _requestFilter = new Filter().With<DamageComponent>();
        _positionFilter = new Filter().With<PositionComponent, HealthComponent>();
    }

    public void Update(float deltaTime)
    {
        foreach (var requestEntity in _requestFilter)
        {
            var damageComponent = requestEntity.GetComponent<DamageComponent>();

            foreach (var entity in _positionFilter)
            {
                var positionComponent = entity.GetComponent<PositionComponent>();

                if (positionComponent.X != damageComponent.PositionX || positionComponent.Y != damageComponent.PositionY)
                {
                    continue;
                }
                
                var healthComponent = entity.GetComponent<HealthComponent>();
                healthComponent.Value -= damageComponent.Damage;
                if (healthComponent.Value <= 0)
                {
                    entity.SetComponent(new KillComponent());
                }
            }
            
            requestEntity.RemoveComponent<DamageComponent>();
        }
    }
}