using NaiveECS.Core;

namespace NaiveECS.Example.Components;

public struct SymbolComponent : IComponent
{
    public char Value;
    public ConsoleColor Color;
}