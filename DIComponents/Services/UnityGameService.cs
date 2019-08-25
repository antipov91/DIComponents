using System;
using System.Collections.Generic;
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
            {
                var childrens = GetChildrens(component.transform);
                var child = childrens.Find(x => x.name == name);
                if (ReferenceEquals(child, null))
                    return null;

                return child.GetComponent(type);
            }
            return go.GetComponent(type);
        }

        public List<GameObject> GetChildrens(Transform transform)
        {
            var childrens = new List<GameObject>();
            for (int i = 0; i < transform.childCount; i++)
            {
                childrens.Add(transform.GetChild(i).gameObject);
                childrens.AddRange(GetChildrens(transform.GetChild(i).transform));
            }
            return childrens;
        }
    }
}