namespace NaiveECS.Core;

public sealed class EntityCache
{
    public HashSet<int> Entities => _entities;
    private HashSet<int> _entities = new();
    private Queue<int> _freedIds = new();
    private int _nextId = int.MinValue + 1;
    public int CreateEntity()
    {
        int entityId = GetUniqueId();
        _entities.Add(entityId);
        return entityId;
    }

    private int GetUniqueId()
    {
        return _freedIds.Count > 0 ? _freedIds.Dequeue() : _nextId++;
    }
    
    private void ReleaseId(int id)
    {
        _freedIds.Enqueue(id);
    }
    
    public bool RemoveEntity(int entity)
    {
        if (!_entities.Contains(entity))
        {
            return false;
        }
        
        _entities.Remove(entity);
        ReleaseId(entity);
        return true;
    }
}