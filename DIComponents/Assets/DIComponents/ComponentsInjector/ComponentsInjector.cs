using System;
using System.Reflection;
using UnityEngine;

namespace DIComponents
{
    public class ComponentsInjector : IComponentsInjector
    {
        public void InjectComponent(Component component, FieldInfo fieldInfo)
        {
            var injectComponentAttribute = Attribute.GetCustomAttribute(fieldInfo, typeof(InjectComponentAttribute)) as InjectComponentAttribute;
            if (ReferenceEquals(injectComponentAttribute, null))
                return;

            var injectedComponent = component.gameObject.transform.GetComponent(fieldInfo.FieldType);
            fieldInfo.SetValue(component, injectedComponent);
        }

        public void InjectComponentFromChild(Component component, FieldInfo fieldInfo)
        {
            var injectComponentFromChild = Attribute.GetCustomAttribute(fieldInfo, typeof(InjectComponentFromChildAttribute)) as InjectComponentFromChildAttribute;
            if (ReferenceEquals(injectComponentFromChild, null))
                return;

            var injectedComponent =  component.gameObject.GetComponentInChildren(fieldInfo.FieldType);
            fieldInfo.SetValue(component, injectedComponent);
        }

        public void InjectComponentFromObject(Component component, FieldInfo fieldInfo)
        {
            var injectComponentFromObject = Attribute.GetCustomAttribute(fieldInfo, typeof(InjectComponentFromObjectAttribute)) as InjectComponentFromObjectAttribute;
            if (ReferenceEquals(injectComponentFromObject, null))
                return;

            var objectTransform = GameObject.Find(injectComponentFromObject.objectName);
            var injectedComponent = objectTransform.GetComponent(fieldInfo.FieldType);
            fieldInfo.SetValue(component, injectedComponent);
        }
    }
}