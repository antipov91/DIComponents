using System.Reflection;
using UnityEngine;

namespace DIComponents
{
    public interface IComponentsInjector
    {
        void InjectComponent(Component component, FieldInfo fieldInfo);
        void InjectComponentFromChild(Component component, FieldInfo fieldInfo);
        void InjectComponentFromObject(Component component, FieldInfo fieldInfo);
    }
}