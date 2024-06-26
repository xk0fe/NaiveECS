﻿using NaiveECS.Core;
using NaiveECS.Example.Roguelike.Common;
using NaiveECS.Example.Roguelike.Components;
using NaiveECS.Example.Roguelike.Constants;
using NaiveECS.Extensions;

namespace NaiveECS.Example.Roguelike.Systems;

public class KillSystem : ISystem
{
    private Filter _filter;
    private Filter _playerFilter;

    private Grid _grid;

    public KillSystem(Grid grid)
    {
        _grid = grid;
    }

    public void Awake()
    {
        _filter = new Filter().With<PositionComponent, KillComponent>();
        _playerFilter = new Filter().With<PlayerComponent>();
    }

    public void Update(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            entity.TryGetComponent(out PositionComponent positionComponent);

            _grid.SetOccupied(positionComponent.X, positionComponent.Y, false);

            foreach (var playerEntity in _playerFilter)
            {
                playerEntity.TryGetComponent(out PlayerComponent playerComponent);
                playerComponent.Experience += GameSettings.EXP_PER_KILL;
                if (playerComponent.Experience >= GameSettings.EXP_PER_LEVEL)
                {
                    playerComponent.Level++;
                    playerComponent.Experience = 0;
                }
                
                playerEntity.SetComponent(playerComponent);
            }
            
            entity.Remove();
        }
    }
}