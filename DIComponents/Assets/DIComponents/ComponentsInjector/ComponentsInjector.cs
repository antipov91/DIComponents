using System;
using System.Reflection;
using UnityEngine;

namespace DIComponents
{
    public class ComponentsInjector : IComponentsInjector
    {
        private IGameService gameService;
        private DIContainer container;
        private DIObjectPooling objectPooling;

        public ComponentsInjector(IGameService gameService)
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
            fieldInfo.SetValue(component, injectedComponent);
        }

        public void InjectComponentFromChild(Component component, FieldInfo fieldInfo)
        {
            var injectComponentFromChild = Attribute.GetCustomAttribute(fieldInfo, typeof(InjectComponentFromChildAttribute)) as InjectComponentFromChildAttribute;
            if (ReferenceEquals(injectComponentFromChild, null))
                return;

            var injectedComponent = gameService.GetComponentInChildren(component, injectComponentFromChild.childName, fieldInfo.FieldType);
            fieldInfo.SetValue(component, injectedComponent);
        }

        public void InjectComponentFromObject(Component component, FieldInfo fieldInfo)
        {
            var injectComponentFromObject = Attribute.GetCustomAttribute(fieldInfo, typeof(InjectComponentFromObjectAttribute)) as InjectComponentFromObjectAttribute;
            if (ReferenceEquals(injectComponentFromObject, null))
                return;

            var componentName = injectComponentFromObject.objectName + fieldInfo.Name;
            if (objectPooling.Contains(componentName))
            {
                var injectedComponent = objectPooling.GetObject(componentName) as Component;
                fieldInfo.SetValue(component, injectedComponent);
            }
            else
            {
                var injectedComponent = gameService.Find(injectComponentFromObject.objectName, fieldInfo.FieldType);
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