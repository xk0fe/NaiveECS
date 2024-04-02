using BenchmarkDotNet.Attributes;
using NaiveECS.Core;
using NaiveECS.Extensions;

namespace NaiveECS.Benchmarks;

[MemoryDiagnoser]
public class MemoryBenchmarkerDemo
{
    int EntityCount = 100000;
    
    [Benchmark]
    public void CreateEntityWithOneComponent()
    {
        var world = World.Default();
        for (int i = 0; i < EntityCount; i++)
        {
            var entity = world.CreateEntity();
            entity.SetComponent(new BenchmarkContext.Component1());
        }
    }
    
    [Benchmark]
    public void CreateEntityWithTwoComponents()
    {
        var world = World.Default();
        for (int i = 0; i < EntityCount; i++)
        {
            var entity = world.CreateEntity();
            entity.SetComponent(new BenchmarkContext.Component1());
            entity.SetComponent(new BenchmarkContext.Component2());
        }
    }
    
    [Benchmark]
    public void CreateEntityWithThreeComponents()
    {
        var world = World.Default();
        for (int i = 0; i < EntityCount; i++)
        {
            var entity = world.CreateEntity();
            entity.SetComponent(new BenchmarkContext.Component1());
            entity.SetComponent(new BenchmarkContext.Component2());
            entity.SetComponent(new BenchmarkContext.Component3());
        }
    }
    
    [Benchmark]
    public void SystemWithOneComponent()
    {
        var filter = new Filter().With<BenchmarkContext.Component1>();
        foreach (var entity in filter)
        {
            var component = entity.GetComponent<BenchmarkContext.Component1>();
            component.Value++;
        }
    }
    
    [Benchmark]
    public void SystemWithTwoComponents()
    {
        var filter = new Filter().With<BenchmarkContext.Component1>().With<BenchmarkContext.Component2>();
        foreach (var entity in filter)
        {
            var component = entity.GetComponent<BenchmarkContext.Component1>();
            var component2 = entity.GetComponent<BenchmarkContext.Component2>();
            component.Value++;
            component2.Value++;
        }
    }
    
    [Benchmark]
    public void SystemWithThreeComponents()
    {
        var filter = new Filter().With<BenchmarkContext.Component1>().With<BenchmarkContext.Component2>().With<BenchmarkContext.Component3>();
        foreach (var entity in filter)
        {
            var component = entity.GetComponent<BenchmarkContext.Component1>();
            var component2 = entity.GetComponent<BenchmarkContext.Component2>();
            var component3 = entity.GetComponent<BenchmarkContext.Component3>();
            component.Value++;
            component2.Value++;
            component3.Value++;
        }
    }
}