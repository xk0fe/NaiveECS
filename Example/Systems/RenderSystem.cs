using NaiveECS.Core;
using NaiveECS.Example.Components;
using NaiveECS.Example.Constants;
using NaiveECS.Extensions;

namespace NaiveECS.Example.Systems;

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
            var position = entity.GetComponent<PositionComponent>();
            var symbol = entity.GetComponent<SymbolComponent>();
            Console.ForegroundColor = symbol.Color;
            Console.SetCursorPosition(position.X, position.Y);
            Console.Write(symbol.Value);
        }
    }
}