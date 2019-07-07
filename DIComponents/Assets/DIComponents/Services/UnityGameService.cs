using System;
using UnityEngine;

namespace DIComponents.Core
{
    public class UnityGameService : IGameService
    {
        public object Find(string name, Type type)
        {
            var go = GameObject.Find(name);
            if (ReferenceEquals(go, null))
                return null;
            return go.GetComponent(type);
        }

        public object GetComponent(object obj, Type type)
        {
            var component = obj as Component;
            return component.GetComponent(type);
        }

        public object GetComponentInChildren(object obj, string name, Type type)
        {
            var component = obj as Component;
            var go = component.transform.Find(name);
            if (ReferenceEquals(go, null))
                return null;
            return go.GetComponent(type);
        }
    }
}
