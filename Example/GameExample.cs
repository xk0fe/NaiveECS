using NaiveECS.Core;
using NaiveECS.Example.Systems;
using NaiveECS.Extensions;

namespace NaiveECS.Example;

public class GameExample
{
    private List<ISystem> _systems = new();
    
    public void Initialize()
    {
        AddSystem(new InitializeWarriorsSystem());
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