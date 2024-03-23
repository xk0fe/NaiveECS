using NaiveECS.Core;
using NUnit.Framework;

namespace NaiveECS.Tests;

[TestFixture]
public class EntityTests
{
    private World _world;

    [SetUp]
    public void Setup()
    {
        _world = World.Default();
    }
    
    [Test(ExpectedResult = 0)]
    public int CreateEntity()
    {
        var entity = _world.CreateEntity();
        Assert.Pass();
        return entity;
    }

    [Test]
    public void RemoveEntity()
    {
        var entity = _world.CreateEntity();
        
        Assert.That(_world.RemoveEntity(entity), "Removing entity failed!");
    }

    [Test]
    public void RemoveEntityTwice()
    {
        var entity = _world.CreateEntity();
        Assert.That(_world.RemoveEntity(entity), "Removing entity failed!");
        Assert.That(!_world.RemoveEntity(entity), "Removing entity that was removed prior succeeded!");
    }
    
    [Test]
    public void RemoveEntityNonExistent()
    {
        Assert.That(!_world.RemoveEntity(1000), "Removing non-existent entity succeeded!");
    }
}