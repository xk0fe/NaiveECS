using NaiveECS.Core;

namespace NaiveECS.Example.Roguelike.Components;

public struct SymbolComponent : IComponent
{
    public char Value;
    public ConsoleColor Color;
}