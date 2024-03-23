using NaiveECS.Core;

namespace NaiveECS.Example.Components;

public struct PlayerComponent : IComponent
{
    public int Level;
    public int Experience;
}