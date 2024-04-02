using NaiveECS.Core;

namespace NaiveECS.Example.Components;

public record NameComponent : IComponent
{
    public string Value;
}