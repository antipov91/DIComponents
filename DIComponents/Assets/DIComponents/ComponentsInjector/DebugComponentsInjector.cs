using System;
using System.Reflection;
using UnityEngine;

namespace DIComponents
{
    public class DebugComponentsInjector : IComponentsInjector
    {
        private IGameService gameService;
        private DIContainer container;
        private DIObjectPooling objectPooling;

        public DebugComponentsInjector(IGameService gameService)
        {
            this.gameService = gameService;
            container = new DIContainer();
            objectPooling = new DIObjectPooling();
        }

        public void InjectComponent(Component component, FieldInfo fieldInfo)
        {
            var injectComponentAttribute = Attribute.GetCustomAttribute(fieldInfo, typeof(InjectComponentAttribute)) as InjectComponentAttribute;
            if (ReferenceEquals(injectComponentAttribute, null))
                return;

            var injectedComponent = gameService.GetComponent(component, fieldInfo.FieldType);
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

            var injectedComponent = gameService.GetComponentInChildren(component, injectComponentFromChild.childName, fieldInfo.FieldType);
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


            var componentName = injectComponentFromObject.objectName + fieldInfo.Name;
            if (objectPooling.Contains(componentName))
            {
                var injectedComponent = objectPooling.GetObject(componentName) as Component;
                fieldInfo.SetValue(component, injectedComponent);
            }
            else
            {
                var injectedComponent = gameService.Find(injectComponentFromObject.objectName, fieldInfo.FieldType);
                if (ReferenceEquals(injectedComponent, null))
                    throw new NullReferenceException(string.Format("Could not find: {0}. Or component {1} at object with name {0} is not added", injectComponentFromObject.objectName, fieldInfo.FieldType));

                objectPooling.AddObject(componentName, injectedComponent);
                fieldInfo.SetValue(component, injectedComponent);
            }
        }

        public void InjectAsSingle(Component component, FieldInfo fieldInfo)
        {
            var injectAsSingle = Attribute.GetCustomAttribute(fieldInfo, typeof(InjectAsSingleAttribute)) as InjectAsSingleAttribute;
            if (ReferenceEquals(injectAsSingle, null))
                return;

            var obj = container.CreateObjectAsSingle(fieldInfo.FieldType);
            fieldInfo.SetValue(component, obj);
        }

        public void InjectAsTransient(Component component, FieldInfo fieldInfo)
        {
            var injectAsTransient = Attribute.GetCustomAttribute(fieldInfo, typeof(InjectAsTransientAttribute)) as InjectAsTransientAttribute;
            if (ReferenceEquals(injectAsTransient, null))
                return;

            var obj = container.CreateObjectAsTransient(fieldInfo.FieldType);
            fieldInfo.SetValue(component, obj);
        }
    }
}