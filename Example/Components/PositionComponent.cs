using NaiveECS.Core;

namespace NaiveECS.Example.Components;

public record PositionComponent : IComponent
{
    public int X { get; set; }
    public int Y { get; set; }
}