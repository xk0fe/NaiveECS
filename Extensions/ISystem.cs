namespace NaiveECS.Extensions;

public interface ISystem
{
    public void Awake();
    public void Update(float deltaTime);
}