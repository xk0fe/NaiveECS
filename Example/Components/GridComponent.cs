using NaiveECS.Core;
using NaiveECS.Example.Common;

namespace NaiveECS.Example.Components;

public record GridComponent : IComponent
{
    public Grid Value;
}