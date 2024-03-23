using NaiveECS.Core;
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
}