using NaiveECS.Core;
using NaiveECS.Example.Components;
using NaiveECS.Extensions;

namespace NaiveECS.Tests;

public class Test
{
    private string[] _testNames = new[]
    {
        "Aleksandr",
        "Ana",
        "Olga",
        "Maria",
        "Ricardo",
        "Guilherme"
    };

    private World? _world;
    
    public Test()
    {
        _world = World.Default();
    }
    
    public void CreateEntity()
    {
        var entity = _world.CreateEntity();
        var random = new Random();
        var name = new NameComponent
        {
            Value = _testNames[random.Next(0, _testNames.Length)],
        };
        entity.SetComponent(name);
        
        Console.WriteLine($"Entity {entity} created!");
    }

    public void GetComponent(int entity)
    {
        if (entity.TryGetComponent<NameComponent>(out var component))
        {
            Console.WriteLine($"Entity {entity} has {nameof(NameComponent)} component with value {component.Value}!");
        }
        else
        {
            Console.WriteLine($"Entity {entity} does NOT contain {nameof(NameComponent)} component!");
        }
    }

    public void ChangeComponent(int entity, string value)
    {
        var component = entity.GetComponent<NameComponent>();
        component.Value = value;
        Console.WriteLine($"Entity {entity} {nameof(NameComponent)} component set new value {component.Value}!");
    }

    public void RemoveEntity(int entity)
    {
        if (entity.Remove())
        {
            Console.WriteLine($"Entity {entity} was removed!");
        }
        else
        {
            Console.WriteLine($"Entity {entity} was NOT removed!");
        }
    }
    
    public void GetAllNames()
    {
        var filter = new Filter().With<NameComponent>().Without<CharacterComponent>();

        foreach (var entity in filter)
        {
            var component = entity.GetComponent<NameComponent>();
            Console.WriteLine($"Entity {entity} name is {component.Value}");
        }
    }
}