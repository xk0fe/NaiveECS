using NaiveECS.Core;

namespace NaiveECS.Extensions;

public static class EntityExtension
{
    public static bool TryGetComponent<T>(this int entity, out T component) where T : struct, IComponent
    {
        return World.Default().ComponentCache.TryGetComponent<T>(entity, out component);
    }
    
    [Obsolete("Use SetComponent() instead")]
    public static void AddComponent(this int entity, ref IComponent component)
    {
        World.Default().ComponentCache.AddComponent(entity, ref component);
    }

    public static void SetComponent<T>(this int entity, ref T component) where T : struct, IComponent
    {
        World.Default().ComponentCache.SetComponent(entity, ref component);
    }

    public static bool RemoveComponent<T>(this int entity, T component) where T : struct, IComponent
    {
        return World.Default().ComponentCache.RemoveComponent(entity, component);
    }

    public static bool Remove(this int entity)
    {
        return World.Default().RemoveEntity(entity);
    }
}