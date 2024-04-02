using NaiveECS.Core;

namespace NaiveECS.Example.Components;

public record SymbolComponent : IComponent
{
    public char Value;
    public ConsoleColor Color;
}