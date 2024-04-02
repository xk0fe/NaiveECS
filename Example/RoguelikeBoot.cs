using NaiveECS.Core;
using NaiveECS.Example.Common;
using NaiveECS.Example.Constants;
using NaiveECS.Example.Interfaces;
using NaiveECS.Example.Systems;
using NaiveECS.Extensions;

namespace NaiveECS.Example;

public class RoguelikeBoot : IBootable
{
    private List<ISystem> _systems = new();
    
    public void Boot()
    {
        Console.Title = "NaiveECS Example - Game";
        
        CreateCharacters();
        InitializeSystems();
    }

    private void CreateCharacters()
    {
        var characterFactory = new CharacterFactory();
        characterFactory.CreatePlayer();
        var random = new Random();
        for (var i = 0; i < random.Next(GameSettings.MIN_NPC_COUNT, GameSettings.MAX_NPC_COUNT); i++)
        {
            characterFactory.CreateNPC(random);
        }
    }

    private void InitializeSystems()
    {
        AddSystem(new NPCMovementSystem());
        AddSystem(new InitializeHealthSystem());
        
        AddSystem(new PlayerMovementSystem());
        AddSystem(new DamageSystem());
        AddSystem(new KillSystem());
        
        AddSystem(new GridSystem());
        AddSystem(new RenderSystem());
        AddSystem(new GameLogSystem());
    }

    private void AddSystem(ISystem system)
    {
        system.Awake();
        _systems.Add(system);
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