using NaiveECS.Core;
using NaiveECS.Example.Components;
using NaiveECS.Extensions;
using NUnit.Framework;

namespace NaiveECS.Tests;

[TestFixture]
public class ComponentTests
{
    private World _world;

    [SetUp]
    public void Setup()
    {
        _world = World.Default();
    }
    
    [Test]
    public void GetComponent()
    {
        var entity = _world.CreateEntity();
        var value = "TestName";
        var name = new NameComponent
        {
            Value = value,
        };
        entity.SetComponent(name);
        
        _world.Commit();

        if (entity.TryGetComponent<NameComponent>(out var component))
        {
            Assert.Pass($"Entity {entity} has {nameof(NameComponent)} component with value {component.Value}!");
        }
        else
        {
            Assert.Fail($"Entity {entity} does NOT contain {nameof(NameComponent)} component!");
        }
    }

    [Test]
    public void SetComponent()
    {
        var entity = _world.CreateEntity();
        var value = "TestName";
        var name = new NameComponent
        {
            Value = value,
        };
        entity.SetComponent(name);
        Assert.Pass($"Entity {entity} {nameof(NameComponent)} component set new value {value}!");
    }
}