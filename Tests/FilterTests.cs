using NaiveECS.Core;
using NaiveECS.Example.Components;
using NaiveECS.Extensions;
using NUnit.Framework;

namespace NaiveECS.Tests;

[TestFixture]
public class FilterTests
{
    private World _world;

    [SetUp]
    public void Setup()
    {
        _world = World.Default();
    }
    
    [Test]
    public void CreateFilter()
    {
        var filter = new Filter();
        Assert.That(filter != null, "Filter creation failed!");
    }
    
    [Test]
    public void FilterTryGetComponent()
    {
        var testEntity = _world.CreateEntity();
        var name = new NameComponent
        {
            Value = "Test"
        };
        testEntity.SetComponent(name);
        
        _world.Commit();
        
        var filter = new Filter(_world);
        filter.With<NameComponent>();
        
        Assert.That(filter != null, "With filter creation failed!");
        Assert.That(filter.Count() > 0, "Filter is empty!");
        foreach (var entity in filter)
        {
            if (entity.TryGetComponent(out NameComponent component))
            {
                Console.WriteLine(component.Value);
            }
        }
    }
    
    [Test]
    public void FilterGetComponent()
    {
        var testEntity = _world.CreateEntity();
        var name = new NameComponent
        {
            Value = "Test"
        };
        testEntity.SetComponent(name);
        
        var filter = new Filter(_world);
        filter.With<NameComponent>();
        
        _world.Commit();
        
        Assert.That(filter != null, "With filter creation failed!");
        Assert.That(filter.Count() > 0, "Filter is empty!");
        foreach (var entity in filter)
        {
            var component = entity.GetComponent<NameComponent>();
            Console.WriteLine(component.Value);
            component.Value = "FilterGetComponent";
            
            entity.SetComponent(component);
        }

        foreach (var entity in filter)
        {
            var component = entity.GetComponent<NameComponent>();
            Console.WriteLine(component.Value);
        }
    }
    
    // todo
    [Test]
    public void FilterGetComponentRef()
    {
        var testEntity = _world.CreateEntity();
        var name = new NameComponent
        {
            Value = "Test"
        };
        testEntity.SetComponent(name);
        
        var filter = new Filter(_world);
        filter.With<NameComponent>();
        
        _world.Commit();

        Assert.That(filter != null, "With filter creation failed!");
        Assert.That(filter.Count() > 0, "Filter is empty!");

        // todo
        // System.ExecutionEngineException: Exception of type 'System.ExecutionEngineException' was thrown.
        foreach (var entity in filter)
        {
            ref var component = ref entity.GetComponentRef<NameComponent>();
            // Console.WriteLine(component.Value);
            component.Value = "FilterGetComponentRef";
        }

        // foreach (var entity in filter)
        // {
        //     var component = entity.GetComponent<NameComponent>();
        //     Console.WriteLine(component.Value);
        // }
    }
}