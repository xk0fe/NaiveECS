using NaiveECS.Core;
using NaiveECS.Example.Components;
using NaiveECS.Example.Constants;
using NaiveECS.Extensions;

namespace NaiveECS.Example.Common;

public class CharacterFactory
{
    public int CreatePlayer()
    {
        var entity = World.Default().CreateEntity();
        var position = new PositionComponent
        {
            X = GameSettings.MAP_WIDTH / 2,
            Y = GameSettings.MAP_HEIGHT / 2
        };
        entity.SetComponent(ref position);
        var symbol = new SymbolComponent
        {
            Value = GameSettings.PLAYER_SYMBOL,
            Color = ConsoleColor.Yellow,
        };
        entity.SetComponent(ref symbol);
        var characterComponent = new CharacterComponent();
        entity.SetComponent(ref characterComponent);
        var playerComponent = new PlayerComponent
        {
            Level = 1,
        };
        entity.SetComponent(ref playerComponent);
        return entity;
    }

    public int CreateNPC(Random random)
    {
        var world = World.Default();
        var entity = world.CreateEntity();
        var position = new PositionComponent
        {
            X = random.Next(0, GameSettings.MAP_WIDTH),
            Y = random.Next(0, GameSettings.MAP_HEIGHT)
        };
        entity.SetComponent(ref position);
        var symbol = new SymbolComponent
        {
            Value = GameSettings.NPC_SYMBOL,
            Color = ConsoleColor.Blue,
        };
        entity.SetComponent(ref symbol);
        var characterComponent = new CharacterComponent();
        entity.SetComponent(ref characterComponent);

        var decisionDelay = new DecisionDelayComponent
        {
            Delay = GameSettings.NPC_DECISION_DELAY,
            CurrentDelay = 0,
        };
        entity.SetComponent(ref decisionDelay);
        return entity;
    }
}