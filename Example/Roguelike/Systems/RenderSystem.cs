using NaiveECS.Core;
using NaiveECS.Example.Roguelike.Components;
using NaiveECS.Extensions;

namespace NaiveECS.Example.Roguelike.Systems;

public class RenderSystem : ISystem
{
    private Filter _filter;
    public void Awake()
    {
        _filter = new Filter().With<PositionComponent>().With<SymbolComponent>();
    }

    public void Update(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            entity.TryGetComponent(out PositionComponent position);
            entity.TryGetComponent(out SymbolComponent symbol);
            Console.ForegroundColor = symbol.Color;
            Console.SetCursorPosition(position.X, position.Y);
            Console.Write(symbol.Value);
        }
    }
}