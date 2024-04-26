using NaiveECS.Core;

namespace NaiveECS.Example.Roguelike.Components;

public struct DecisionDelayComponent : IComponent
{
    public float Delay;
    public float CurrentDelay;
}