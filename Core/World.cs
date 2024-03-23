namespace NaiveECS.Core;

public class World
{
    public EntityCache EntityCache { get; } = new();
    public ComponentCache ComponentCache { get; } = new();
    
    public static World Default()
    {
        return _default ??= new World();
    }
    
    private static World? _default = null;
    
    public int CreateEntity()
    {
        return EntityCache.CreateEntity();
    }
    
    public bool RemoveEntity(int entity)
    {
        if (!EntityCache.RemoveEntity(entity))
        {
            return false;
        }
        
        ComponentCache.Components.Remove(entity);
        return true;
    }
    
    public void Commit()
    {
        ComponentCache.Commit();
    }
}