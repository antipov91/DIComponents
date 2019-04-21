using System;
using UnityEngine;

namespace DIComponents
{
    public interface IGameService
    {
        Component GetComponent(Component component, Type type);
        Component GetComponentInChildren(Component component, string name, Type type);
        Component Find(string name, Type type);
    }
}
