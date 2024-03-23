using NaiveECS.Core;
using NaiveECS.Example.Components;
using NaiveECS.Example.Constants;
using NaiveECS.Extensions;

namespace NaiveECS.Example.Systems;

public class GameLogSystem : ISystem
{
    private Filter _npcFilter;
    public void Awake()
    {
        _npcFilter = new Filter().With<CharacterComponent>();
    }

    public void Update(float deltaTime)
    {
        Console.SetCursorPosition(0, GameSettings.MAP_HEIGHT);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("NPCs ACTIVE: " + _npcFilter.Count());
    }

    public void Dispose()
    {
    }
}