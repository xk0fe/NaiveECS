using NaiveECS.Core;
using NaiveECS.Example.Roguelike.Common;
using NaiveECS.Example.Roguelike.Components;
using NaiveECS.Example.Roguelike.Constants;
using NaiveECS.Extensions;

namespace NaiveECS.Example.Roguelike.Systems;

public class PlayerMovementSystem : ISystem
{
    private Filter _filter;

    private Grid _grid;

    public PlayerMovementSystem(Grid grid)
    {
        _grid = grid;
    }
    
    public void Awake()
    {
        _filter = new Filter().With<PlayerComponent>().With<PositionComponent>();
    }

    private void OnMoveCharacter(int x, int y)
    {
        foreach (var entity in _filter)
        {
            entity.TryGetComponent(out PositionComponent position);
            var previousX = position.X;
            var previousY = position.Y;
            position.X += x;
            position.Y += y;

            if (!_grid.CanMoveTo(position.X, position.Y, out var reason))
            {
                if (reason == CellBlockedReason.Occupied)
                {
                    var damageEntity = World.Default().CreateEntity();
                    var damage = new DamageComponent
                    {
                        Damage = GameSettings.PLAYER_DAMAGE,
                        PositionX = position.X,
                        PositionY = position.Y,
                    };
                    damageEntity.SetComponent(damage);
                }

                continue;
            }

            _grid.SetOccupied(previousX, previousY, false);
            _grid.SetOccupied(position.X, position.Y, true);

            entity.SetComponent(position);
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