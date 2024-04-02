using NaiveECS.Core;
using NaiveECS.Example.Interfaces;
using NaiveECS.Example.Roguelike.Common;
using NaiveECS.Example.Roguelike.Constants;
using NaiveECS.Example.Roguelike.Systems;
using NaiveECS.Extensions;

namespace NaiveECS.Example.Roguelike;

public class RoguelikeBoot : IBootable
{
    private List<ISystem> _systems = new();
    private Grid _grid;
    
    public void Boot()
    {
        Console.Title = "NaiveECS Example - Game";
        
        CreateCharacters();
        _grid = CreateGrid();
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

    private Grid CreateGrid()
    {
        var grid = new Grid(GameSettings.MAP_WIDTH, GameSettings.MAP_HEIGHT);

        var random = new Random();
        var randomCount = random.Next(50, 150);
        while (randomCount > 0)
        {
            randomCount--;

            grid.SetObstacle(random.Next(0, 100), random.Next(0, 10));
        }

        return grid;
    }

    private void InitializeSystems()
    {
        AddSystem(new NPCMovementSystem(_grid));
        AddSystem(new InitializeHealthSystem());
        
        AddSystem(new PlayerMovementSystem(_grid));
        AddSystem(new DamageSystem());
        AddSystem(new KillSystem(_grid));
        
        AddSystem(new GridSystem(_grid));
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