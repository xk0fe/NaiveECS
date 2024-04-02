namespace NaiveECS.Example.Interfaces;

public interface IBootable
{
    public void Boot();
    public void Run(float deltaTime);
}