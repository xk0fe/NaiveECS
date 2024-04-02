using NaiveECS.Core;

namespace NaiveECS.Example.Roguelike.Components;

public struct PositionComponent : IComponent
{
    public int X;
    public int Y;
}