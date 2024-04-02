using NaiveECS.Core;

namespace NaiveECS.Example.Roguelike.Components;

public struct PlayerComponent : IComponent
{
    public int Level;
    public int Experience;
}