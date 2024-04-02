namespace NaiveECS.Core;

public record DecisionDelayComponent : IComponent
{
    public float Delay;
    public float CurrentDelay;
}