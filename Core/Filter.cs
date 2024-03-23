using System.Collections;

namespace NaiveECS.Core;

public class Filter : IEnumerable<int>
{
    private List<int> Entities = new List<int>();
    private List<Type> WithFilter = new List<Type>();
    private List<Type> WithoutFilter = new List<Type>();
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
        foreach (var (entity, components) in _componentCache.Components)
        {
            var filterPassed = true;
            for (var i = 0; i < WithFilter.Count; i++)
            {
                var type = WithFilter[i];
                if (components.ContainsKey(type)) continue;
                filterPassed = false;
                break;
            }

            for (var i = 0; i < WithoutFilter.Count; i++)
            {
                var type = WithoutFilter[i];
                if (!components.ContainsKey(type)) continue;
                filterPassed = false;
                break;
            }

            if (filterPassed)
            {
                Entities.Add(entity);
            }
        }

        return Entities.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}