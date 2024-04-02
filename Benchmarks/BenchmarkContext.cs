using NaiveECS.Core;

namespace NaiveECS.Benchmarks;

public class BenchmarkContext
{
    public struct Component1 : IComponent
    {
        public int Value;
    }

    public struct Component2 : IComponent
    {
        public int Value;
    }

    public struct Component3 : IComponent
    {
        public int Value;
    }

    public World World { get; }

    public BenchmarkContext()
    {
        World = World.Default();
    }
}