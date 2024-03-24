using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NaiveECS.Core;

public sealed class ComponentCache
{
    public Dictionary<Type, Dictionary<int, IComponent>> Components = new();
    
    public Dictionary<Type, Dictionary<int, IComponent>> _addQueue = new();
    public Dictionary<int, HashSet<Type>> _removeQueue = new();
    public HashSet<int> _removeEntitiesQueue = new();

    public void SetComponent<T>(int entity, ref T component) where T : struct, IComponent
    {
        if (_addQueue.TryGetValue(component.GetType(), out var entities))
        {
            entities[entity] = component;
        }
        else
        {
            _addQueue[component.GetType()] = new Dictionary<int, IComponent>()
            {
                { entity, component }
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

        if (!Components.TryGetValue(typeof(T), out var entities)) return false;
        
        if (entities.TryGetValue(entity, out var objComponent) && objComponent is T)
        {
            component = (T)objComponent;
            return true;
        }

        return false;
    }
    
    public ref T GetComponentRef<T>(int entity) where T : struct, IComponent
    {
        if (!Components.TryGetValue(typeof(T), out var components)) throw new InvalidOperationException("Entity does not exist in the component dictionary.");
    
        ref var value = ref CollectionsMarshal.GetValueRefOrNullRef(components, entity);
        if (value is T)
        {
            return ref Unsafe.As<IComponent, T>(ref value);
        }
        throw new InvalidOperationException($"Component of type {typeof(T)} does not exist for entity {entity}.");
    }
    
    public T GetComponent<T>(int entity) where T : struct, IComponent
    {
        if (!Components.TryGetValue(typeof(T), out var entities))
        {
            throw new InvalidOperationException("Entity does not exist in the component dictionary.");
        }

        if (entities.TryGetValue(entity, out var value) && value is T)
        {
            return (T)value;
        }

        throw new InvalidOperationException($"Component of type {typeof(T)} does not exist for entity {entity}.");
    }

    public void Commit()
    {
        foreach (var (entity, removeType) in _removeQueue)
        {
            foreach (var type in removeType)
            {
                if (!Components.TryGetValue(type, out var entities))
                {
                    continue;
                }
                entities.Remove(entity);
            }
        }
        
        foreach (var (type, queuedEntities) in _addQueue)
        {
            if (Components.TryGetValue(type, out var entities))
            {
                foreach (var (queuedEntity, queuedComponent) in queuedEntities)
                {
                    entities[queuedEntity] = queuedComponent;
                }
            }
            else
            {
                Components[type] = queuedEntities;
            }
        }
        _addQueue.Clear();
    }

    public void MarkEntityForRemoval(int entity)
    {
        _removeEntitiesQueue.Add(entity);
    }
    
    public void RemoveAllEntityComponentsSlow()
    {
        foreach (var entity in _removeEntitiesQueue)
        {
            foreach (var (type, entities) in Components)
            {
                entities.Remove(entity);
            }
        }
    }
}