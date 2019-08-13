namespace DIComponents.Core
{
    public interface IObjectActivator
    {
        object Create(params object[] args);
    }
}