using NaiveECS.Core;

namespace NaiveECS.Example.Components;

public record HealthComponent : IComponent
{
    public float Value;
}