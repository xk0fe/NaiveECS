using NaiveECS.Core;

namespace NaiveECS.Example.Components;

public record PlayerComponent : IComponent
{
    public int Level;
    public int Experience;
}