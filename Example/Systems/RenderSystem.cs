﻿using NaiveECS.Core;
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
            entity.TryGetComponent(out PositionComponent position);
            entity.TryGetComponent(out SymbolComponent symbol);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(position.X, position.Y);
            Console.Write(symbol.Value);
        }
    }

    public void Dispose()
    {
    }
}