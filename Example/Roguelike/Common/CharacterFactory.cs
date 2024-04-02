using NaiveECS.Core;
using NaiveECS.Example.Roguelike.Components;
using NaiveECS.Example.Roguelike.Constants;
using NaiveECS.Extensions;

namespace NaiveECS.Example.Roguelike.Common;

public class CharacterFactory
{
    public int CreatePlayer()
    {
        var x = GameSettings.MAP_WIDTH / 2;
        var y = GameSettings.MAP_HEIGHT / 2;
        var entity = CreateCharacter(GameSettings.PLAYER_SYMBOL, x, y, ConsoleColor.Yellow);
        entity.SetComponent(new PlayerComponent
        {
            Level = 1,
        });
        return entity;
    }

    public int CreateNPC(Random random)
    {
        var x = random.Next(0, GameSettings.MAP_WIDTH);
        var y = random.Next(0, GameSettings.MAP_HEIGHT);
        var entity = CreateCharacter(GameSettings.NPC_SYMBOL, x, y, ConsoleColor.Blue);
        entity.SetComponent(new DecisionDelayComponent
        {
            Delay = GameSettings.NPC_DECISION_DELAY,
            CurrentDelay = 0,
        });
        return entity;
    }

    private int CreateCharacter(char symbol, int x, int y, ConsoleColor color)
    {
        var world = World.Default();
        var entity = world.CreateEntity();
        entity.SetComponent(new PositionComponent
        {
            X = x,
            Y = y
        });
        entity.SetComponent(new CharacterComponent());
        entity.SetComponent(new SymbolComponent
        {
            Value = symbol,
            Color = color,
        });

        return entity;
    }
}