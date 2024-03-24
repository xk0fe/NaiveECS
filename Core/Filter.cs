﻿using System.Collections;

namespace NaiveECS.Core;

public sealed class Filter : IEnumerable<int>
{
    private HashSet<int> Entities = new();
    private HashSet<Type> WithFilter = new();
    private HashSet<Type> WithoutFilter = new();
    private ComponentCache _componentCache;

    public Filter(World world)
    {
        _componentCache = world.ComponentCache;
    }

    public Filter()
    {
        var world = World.Default();
        _componentCache = world.ComponentCache;
    }

    public Filter With<T>()
    {
        return With(typeof(T));
    }

    public Filter With<T1, T2>()
    {
        return With(typeof(T1), typeof(T2));
    }

    public Filter With<T1, T2, T3>()
    {
        return With(typeof(T1), typeof(T2), typeof(T3));
    }

    public Filter With(params Type[] type)
    {
        for (var i = 0; i < type.Length; i++)
        {
            WithFilter.Add(type[i]);
        }

        return this;
    }

    public Filter Without<T>()
    {
        return Without(typeof(T));
    }

    public Filter Without<T1, T2>()
    {
        return Without(typeof(T1), typeof(T2));
    }

    public Filter Without<T1, T2, T3>()
    {
        return Without(typeof(T1), typeof(T2), typeof(T3));
    }

    public Filter Without(params Type[] type)
    {
        for (var i = 0; i < type.Length; i++)
        {
            WithoutFilter.Add(type[i]);
        }

        return this;
    }

    public IEnumerator<int> GetEnumerator()
    {
        Entities.Clear();

        var components = _componentCache.Components;
        
        foreach (var with in WithFilter)
        {
            if (components.TryGetValue(with, out var entities))
            {
                if (Entities.Count == 0)
                {
                    Entities.UnionWith(entities.Keys);
                }
                else
                {
                    Entities.IntersectWith(entities.Keys);
                }
            }
            else
            {
                Entities.Clear();
                break;
            }
        }

        foreach (var type in WithoutFilter)
        {
            if (!components.TryGetValue(type, out var entities))
            {
                continue;
            }

            Entities.ExceptWith(entities.Keys);
        }
        
        return Entities.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}