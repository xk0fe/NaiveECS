using NaiveECS.Core;
using NaiveECS.Example.Components;
using NaiveECS.Example.Constants;
using NaiveECS.Extensions;

namespace NaiveECS.Example.Common;

public class CharacterFactory
{
    public int CreatePlayer()
    {
        var x = GameSettings.MAP_WIDTH / 2;
        var y = GameSettings.MAP_HEIGHT / 2;
        var entity = CreateCharacter(GameSettings.PLAYER_SYMBOL, x, y, ConsoleColor.Yellow);
        var playerComponent = new PlayerComponent
        {
            Level = 1,
        };
        entity.SetComponent(ref playerComponent);
        return entity;
    }

    public int CreateNPC(Random random)
    {
        var x = random.Next(0, GameSettings.MAP_WIDTH);
        var y = random.Next(0, GameSettings.MAP_HEIGHT);
        var entity = CreateCharacter(GameSettings.NPC_SYMBOL, x, y, ConsoleColor.Blue);
        var decisionDelay = new DecisionDelayComponent
        {
            Delay = GameSettings.NPC_DECISION_DELAY,
            CurrentDelay = 0,
        };
        entity.SetComponent(ref decisionDelay);
        return entity;
    }

    private int CreateCharacter(char symbol, int x, int y, ConsoleColor color)
    {
        var world = World.Default();
        var entity = world.CreateEntity();
        var positionComponent = new PositionComponent
        {
            X = x,
            Y = y
        };
        entity.SetComponent(ref positionComponent);
        var characterComponent = new CharacterComponent();
        entity.SetComponent(ref characterComponent);
        var symbolComponent = new SymbolComponent
        {
            Value = symbol,
            Color = color,
        };
        entity.SetComponent(ref symbolComponent);

        return entity;
    }
}