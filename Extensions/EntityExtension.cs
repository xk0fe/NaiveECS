using NaiveECS.Core;

namespace NaiveECS.Extensions;

public static class EntityExtension
{
    public static ref T GetComponentRef<T>(this int entity) where T : class, IComponent
    {
        return ref World.Default().ComponentCache.GetComponentRef<T>(entity);
    }
    
    public static T GetComponent<T>(this int entity) where T : class, IComponent
    {
        return World.Default().ComponentCache.GetComponent<T>(entity);
    }
    
    public static bool TryGetComponent<T>(this int entity, out T component) where T : class, IComponent
    {
        return World.Default().ComponentCache.TryGetComponent(entity, out component);
    }

    public static void SetComponent<T>(this int entity, T component) where T : class, IComponent
    {
        World.Default().ComponentCache.SetComponent(entity, component);
    }

    public static void RemoveComponent<T>(this int entity) where T : class, IComponent
    {
        World.Default().ComponentCache.RemoveComponent<T>(entity);
    }

    public static bool Remove(this int entity)
    {
        return World.Default().RemoveEntity(entity);
    }
}