using NaiveECS.Core;

namespace NaiveECS.Example.Components;

public struct DamageComponent : IComponent
{
    public int Damage;
    public int PositionX;
    public int PositionY;
}