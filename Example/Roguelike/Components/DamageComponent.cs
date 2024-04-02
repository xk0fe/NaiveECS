using NaiveECS.Core;

namespace NaiveECS.Example.Roguelike.Components;

public struct DamageComponent : IComponent
{
    public int Damage;
    public int PositionX;
    public int PositionY;
}