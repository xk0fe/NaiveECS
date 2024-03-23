using NaiveECS.Core;
using NaiveECS.Example.Common;
using NaiveECS.Example.Constants;
using NaiveECS.Example.Systems;
using NaiveECS.Extensions;

namespace NaiveECS.Example;

public class GameExample
{
    private List<ISystem> _systems = new();
    
    public void Initialize()
    {
        Console.Title = "NaiveECS Example - Game";

        var characterFactory = new CharacterFactory();
        characterFactory.CreatePlayer();
        var random = new Random();
        for (var i = 0; i < random.Next(GameSettings.MIN_NPC_COUNT, GameSettings.MAX_NPC_COUNT); i++)
        {
            characterFactory.CreateNPC(random);
        }
        
        AddSystem(new NPCMovementSystem());
        AddSystem(new InitializeHealthSystem());
        
        AddSystem(new PlayerMovementSystem());
        
        AddSystem(new GridSystem());
        AddSystem(new RenderSystem());
        AddSystem(new GameLogSystem());
        // AddSystem(new MovementSystem());
        // AddSystem(new RenderSystem());
    }

    private void AddSystem(ISystem system)
    {
        system.Awake();
        _systems.Add(system);
    }

    private void RemoveSystem(ISystem system)
    {
        system.Dispose();
        _systems.Remove(system);
    }
    
    public void Run(float deltaTime)
    {
        foreach (var system in _systems)
        {
            system.Update(deltaTime);
            World.Default().Commit();
        }
    }
}