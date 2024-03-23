using NaiveECS.Core;
using NaiveECS.Example.Components;
using NaiveECS.Example.Constants;
using NaiveECS.Extensions;

namespace NaiveECS.Example.Systems;

public class GameLogSystem : ISystem
{
    private Filter _npcFilter;
    private Filter _playerFilter;
    public void Awake()
    {
        _npcFilter = new Filter().With<CharacterComponent>();
        _playerFilter = new Filter().With<PlayerComponent, HealthComponent>();
    }

    public void Update(float deltaTime)
    {
        Console.SetCursorPosition(0, GameSettings.MAP_HEIGHT);
        Console.ForegroundColor = ConsoleColor.White;
        foreach (var entity in _playerFilter)
        {
            entity.TryGetComponent(out PlayerComponent playerComponent);
            entity.TryGetComponent(out HealthComponent healthComponent);
            Console.WriteLine($"NPCs ACTIVE: {_npcFilter.Count()} | LEVEL: {playerComponent.Level} | EXP: {playerComponent.Experience} | HP: {healthComponent.Value} | DMG: {GameSettings.PLAYER_DAMAGE}");
        }
    }
}