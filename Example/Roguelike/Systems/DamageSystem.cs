﻿using NaiveECS.Core;
using NaiveECS.Example.Roguelike.Components;
using NaiveECS.Extensions;

namespace NaiveECS.Example.Roguelike.Systems;

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
                    entity.SetComponent(killComponent);
                }
                    
                entity.SetComponent(healthComponent);
            }
            
            requestEntity.RemoveComponent<DamageComponent>();
        }
    }
}