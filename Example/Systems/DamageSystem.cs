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
            requestEntity.TryGetComponent(out DamageComponent damageComponent);

            foreach (var entity in _positionFilter)
            {
                entity.TryGetComponent(out PositionComponent positionComponent);

                if (positionComponent.X != damageComponent.PositionX || positionComponent.Y != damageComponent.PositionY)
                {
                    continue;
                }
                
                entity.TryGetComponent(out HealthComponent healthComponent);
                healthComponent.Value -= damageComponent.Damage;
                if (healthComponent.Value <= 0)
                {
                    var killComponent = new KillComponent();
                    entity.SetComponent(ref killComponent);
                }
                    
                entity.SetComponent(ref healthComponent);
            }
            
            requestEntity.RemoveComponent<DamageComponent>();
        }
    }
}