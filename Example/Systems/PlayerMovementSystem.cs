using NaiveECS.Core;
using NaiveECS.Example.Components;
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
            gridEntity.TryGetComponent(out GridComponent gridComponent);
            foreach (var entity in _filter)
            {
                entity.TryGetComponent(out PositionComponent position);
                position.X += x;
                position.Y += y;

                if (!gridComponent.Value.CanMoveTo(position.X, position.Y))
                {
                    continue;
                }
                
                entity.SetComponent(ref position);
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

    public void Dispose()
    {
    }
}