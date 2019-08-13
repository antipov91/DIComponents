using System;

namespace DIComponents.Core
{
    public interface IGameService
    {
        object GetComponent(object obj, Type type);
        object GetComponentInChildren(object obj, string name, Type type);
        object Find(string name, Type type);
    }
}
