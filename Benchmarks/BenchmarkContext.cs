using NaiveECS.Core;

namespace NaiveECS.Benchmarks;

public class BenchmarkContext
{
    public record Component1 : IComponent
    {
        public int Value;
    }

    public record Component2 : IComponent
    {
        public int Value;
    }

    public record Component3 : IComponent
    {
        public int Value;
    }

    public World World { get; }

    public BenchmarkContext()
    {
        World = World.Default();
    }
}