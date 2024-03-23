namespace NaiveECS.Core;

public class ComponentCache
{
    public Dictionary<int, Dictionary<Type, IComponent>> Components = new();
    
    [Obsolete("Use SetComponent() instead")]
    public void AddComponent(int entity, ref IComponent component)
    {
        if (Components.TryGetValue(entity, out var components))
        {
            components.Add(component.GetType(), component);
        }
        else
        {
            Components[entity] = new Dictionary<Type, IComponent>()
            {
                {component.GetType(), component}
            };
        }
    }
    
    public void SetComponent<T>(int entity, ref T component) where T : struct, IComponent
    {
        if (Components.TryGetValue(entity, out var components))
        {
            components[component.GetType()] = component;
        }
        else
        {
            Components[entity] = new Dictionary<Type, IComponent>()
            {
                { component.GetType(), component }
            };
        }
    }

    public bool RemoveComponent<T>(int entity, T component) where T : struct, IComponent
    {
        if (!Components.TryGetValue(entity, out var components))
        {
            return false;
        }
        var type = component.GetType();
        if (!components.ContainsKey(type))
        {
            return false;
        }
        
        components.Remove(type);
        return true;

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
}