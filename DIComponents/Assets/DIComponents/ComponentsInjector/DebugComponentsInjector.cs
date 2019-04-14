using System;
using System.Reflection;
using UnityEngine;

namespace DIComponents
{
    public class DebugComponentsInjector : IComponentsInjector
    {
        public void InjectComponent(Component component, FieldInfo fieldInfo)
        {
            var injectComponentAttribute = Attribute.GetCustomAttribute(fieldInfo, typeof(InjectComponentAttribute)) as InjectComponentAttribute;
            if (ReferenceEquals(injectComponentAttribute, null))
                return;

            var injectedComponent = component.gameObject.transform.GetComponent(fieldInfo.FieldType);
            if (ReferenceEquals(injectedComponent, null))
                throw new MissingComponentException(string.Format("Component {0} is not added", fieldInfo.FieldType));

            fieldInfo.SetValue(component, injectedComponent);
        }

        public void InjectComponentFromChild(Component component, FieldInfo fieldInfo)
        {
            var injectComponentFromChild = Attribute.GetCustomAttribute(fieldInfo, typeof(InjectComponentFromChildAttribute)) as InjectComponentFromChildAttribute;
            if (ReferenceEquals(injectComponentFromChild, null))
                return;

            if (string.IsNullOrWhiteSpace(injectComponentFromChild.childName))
                throw new ArgumentException("Attribute value is empty", "childName");
       
            var childTransform = component.gameObject.transform.Find(injectComponentFromChild.childName);
            if (ReferenceEquals(childTransform, null))
                throw new NullReferenceException(string.Format("Child object with name {0} is not added", injectComponentFromChild.childName));

            var injectedComponent = childTransform.GetComponent(fieldInfo.FieldType);
            if (ReferenceEquals(injectedComponent, null))
                throw new MissingComponentException(string.Format("Component {0} at object with name {1} is not added", fieldInfo.FieldType, injectComponentFromChild.childName));
        
            fieldInfo.SetValue(component, injectedComponent);
        }

        public void InjectComponentFromObject(Component component, FieldInfo fieldInfo)
        {
            var injectComponentFromObject = Attribute.GetCustomAttribute(fieldInfo, typeof(InjectComponentFromObjectAttribute)) as InjectComponentFromObjectAttribute;
            if (ReferenceEquals(injectComponentFromObject, null))
                return;

            if (string.IsNullOrWhiteSpace(injectComponentFromObject.objectName))
                throw new ArgumentException("Attribute value is empty", "objectName");

            var objectTransform = GameObject.Find(injectComponentFromObject.objectName);
            if (ReferenceEquals(objectTransform, null))
                throw new NullReferenceException(string.Format("Could not find: {0}", injectComponentFromObject.objectName));

            var injectedComponent = objectTransform.GetComponent(fieldInfo.FieldType);
            if (ReferenceEquals(injectedComponent, null))
                throw new MissingComponentException(string.Format("Component {0} at object with name {1} is not added", fieldInfo.FieldType, injectComponentFromObject.objectName));
           
            fieldInfo.SetValue(component, injectedComponent);
        }
    }
}