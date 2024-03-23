using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NaiveECS.Core;

public sealed class ComponentCache
{
    public Dictionary<int, Dictionary<Type, IComponent>> Components = new();
    
    public Dictionary<int, Dictionary<Type, IComponent>> _addQueue = new();
    public Dictionary<int, HashSet<Type>> _removeQueue = new();

    public void SetComponent<T>(int entity, ref T component) where T : struct, IComponent
    {
        if (_addQueue.TryGetValue(entity, out var components))
        {
            components[component.GetType()] = component;
        }
        else
        {
            _addQueue[entity] = new Dictionary<Type, IComponent>()
            {
                { component.GetType(), component }
            };
        }
    }

    public void RemoveComponent<T>(int entity) where T : struct, IComponent
    {
        if (_removeQueue.TryGetValue(entity, out var components))
        {
            components.Add(typeof(T));
        }
        else
        {
            _removeQueue[entity] = [typeof(T)];
        }
    }

    public bool TryGetComponent<T>(int entity, out T component) where T : struct, IComponent
    {
        component = default;

        if (!Components.TryGetValue(entity, out var components)) return false;
        
        if (components.TryGetValue(typeof(T), out var objComponent) && objComponent is T)
        {
            component = (T)objComponent;
            return true;
        }

        return false;
    }
    
    public ref T GetComponentRef<T>(int entity) where T : struct, IComponent
    {
        if (!Components.TryGetValue(entity, out var components)) throw new InvalidOperationException("Entity does not exist in the component dictionary.");
    
        ref var value = ref CollectionsMarshal.GetValueRefOrNullRef(components, typeof(T));
        if (value is T)
        {
            return ref Unsafe.As<IComponent, T>(ref value);
        }
        throw new InvalidOperationException($"Component of type {typeof(T)} does not exist for entity {entity}.");
    }
    
    public T GetComponent<T>(int entity) where T : struct, IComponent
    {
        if (!Components.TryGetValue(entity, out var components))
        {
            throw new InvalidOperationException("Entity does not exist in the component dictionary.");
        }

        if (components.TryGetValue(typeof(T), out var value) && value is T)
        {
            return (T)value;
        }

        throw new InvalidOperationException($"Component of type {typeof(T)} does not exist for entity {entity}.");
    }

    public void Commit()
    {
        foreach (var (entity, removeType) in _removeQueue)
        {
            if (!Components.TryGetValue(entity, out var components))
            {
                continue;
            }
            
            foreach (var type in removeType)
            {
                components.Remove(type);
            }
        }
        
        foreach (var (entity, queuedComponents) in _addQueue)
        {
            if (Components.TryGetValue(entity, out var components))
            {
                foreach (var (queuedType, queuedComponent) in queuedComponents)
                {
                    components[queuedType] = queuedComponent;
                }
            }
            else
            {
                Components[entity] = queuedComponents;
            }
        }
        _addQueue.Clear();
    }
}