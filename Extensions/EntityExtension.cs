using NaiveECS.Core;

namespace NaiveECS.Extensions;

public static class EntityExtension
{
    public static ref T GetComponentRef<T>(this int entity) where T : struct, IComponent
    {
        return ref World.Default().ComponentCache.GetComponentRef<T>(entity);
    }
    
    public static T GetComponent<T>(this int entity) where T : struct, IComponent
    {
        return World.Default().ComponentCache.GetComponent<T>(entity);
    }
    
    public static bool TryGetComponent<T>(this int entity, out T component) where T : struct, IComponent
    {
        return World.Default().ComponentCache.TryGetComponent(entity, out component);
    }

    public static void SetComponent<T>(this int entity, ref T component) where T : struct, IComponent
    {
        World.Default().ComponentCache.SetComponent(entity, ref component);
    }

    public static void RemoveComponent<T>(this int entity, T component) where T : struct, IComponent
    {
        World.Default().ComponentCache.RemoveComponent(entity, component);
    }

    public static bool Remove(this int entity)
    {
        return World.Default().RemoveEntity(entity);
    }
}