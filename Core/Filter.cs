using System.Collections;

namespace NaiveECS.Core;

public sealed class Filter : IEnumerable<int>
{
    private HashSet<int> _entities = new(256);
    private HashSet<Type> _withFilter = new(8);
    private HashSet<Type> _withoutFilter = new(8);
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
            _withFilter.Add(type[i]);
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
            _withoutFilter.Add(type[i]);
        }

        return this;
    }

    public IEnumerator<int> GetEnumerator()
    {
        _entities.Clear();

        var components = _componentCache.Components;
        
        foreach (var with in _withFilter)
        {
            if (components.TryGetValue(with, out var entities))
            {
                if (_entities.Count == 0)
                {
                    _entities.UnionWith(entities.Keys);
                }
                else
                {
                    _entities.IntersectWith(entities.Keys);
                }
            }
            else
            {
                _entities.Clear();
                break;
            }
        }

        foreach (var type in _withoutFilter)
        {
            if (!components.TryGetValue(type, out var entities))
            {
                continue;
            }

            _entities.ExceptWith(entities.Keys);
        }
        
        return _entities.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}