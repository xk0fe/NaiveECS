using NaiveECS.Core;
using NaiveECS.Example.Common;
using NaiveECS.Example.Components;
using NaiveECS.Example.Constants;
using NaiveECS.Extensions;

namespace NaiveECS.Example.Systems;

public class PlayerMovementSystem : ISystem
{
    private Filter _filter;
    private Filter _gridFilter;
    
    public void Awake()
    {
        _filter = new Filter().With<PlayerComponent>().With<PositionComponent>();
        _gridFilter = new Filter().With<GridComponent>();
    }

    private void OnMoveCharacter(int x , int y)
    {
        foreach (var gridEntity in _gridFilter)
        {
            var gridComponent = gridEntity.GetComponent<GridComponent>();
            var grid = gridComponent.Value;
            foreach (var entity in _filter)
            {
                var position = entity.GetComponent<PositionComponent>();

                if (!grid.CanMoveTo(position.X + x, position.Y + y, out var reason))
                {
                    if (reason == CellBlockedReason.Occupied)
                    {
                        var damageEntity = World.Default().CreateEntity();
                        damageEntity.SetComponent(new DamageComponent
                        {
                            Damage = GameSettings.PLAYER_DAMAGE,
                            PositionX = position.X + x,
                            PositionY = position.Y + y,
                        });
                    }
                    
                    continue;
                }
                
                var previousX = position.X;
                var previousY = position.Y;
                position.X += x;
                position.Y += y;
                grid.SetOccupied(previousX, previousY, false);
                grid.SetOccupied(position.X, position.Y, true);
            }
        }
    }

    public void Update(float deltaTime)
    {
        if (!Console.KeyAvailable)
        {
            return;
        }
        var input = Console.ReadKey(false);

        switch (input.Key)
        {
            case ConsoleKey.LeftArrow:
                OnMoveCharacter(-1, 0);
                break;
            case ConsoleKey.RightArrow:
                OnMoveCharacter(1, 0);
                break;
            case ConsoleKey.UpArrow:
                OnMoveCharacter(0, -1);
                break;
            case ConsoleKey.DownArrow:
                OnMoveCharacter(0, 1);
                break;
        }
    }
}