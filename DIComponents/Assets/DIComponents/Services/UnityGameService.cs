using System;
using UnityEngine;

namespace DIComponents
{
    public class UnityGameService : IGameService
    {
        public Component Find(string name, Type type)
        {
            var go = GameObject.Find(name);
            if (ReferenceEquals(go, null))
                return null;
            return go.GetComponent(type);
        }

        public Component GetComponent(Component component, Type type)
        {
            return component.GetComponent(type);
        }

        public Component GetComponentInChildren(Component component, string name, Type type)
        {
            var go = component.transform.Find(name);
            if (ReferenceEquals(go, null))
                return null;
            return go.GetComponent(type);
        }
    }
}
