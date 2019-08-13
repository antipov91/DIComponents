using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace DIComponents.Core
{
    public class DebugInjector : IInjector
    {
        private IGameService gameService;
        private DIContainer container;
        private DIObjectPooling objectPooling;

        public DebugInjector(IGameService gameService)
        {
            this.gameService = gameService;
            container = new DIContainer();
            objectPooling = new DIObjectPooling();
        }

        public void InjectComponent(object obj, FieldInfo fieldInfo)
        {
            var injectComponentAttribute = Attribute.GetCustomAttribute(fieldInfo, typeof(InjectComponentAttribute)) as InjectComponentAttribute;
            if (ReferenceEquals(injectComponentAttribute, null))
                return;

            var injectedComponent = gameService.GetComponent(obj, fieldInfo.FieldType);
            if (ReferenceEquals(injectedComponent, null))
                throw new MissingComponentException(string.Format("Component {0} is not added", fieldInfo.FieldType));

            fieldInfo.SetValue(obj, injectedComponent);
        }

        public void InjectComponentFromChild(object obj, FieldInfo fieldInfo)
        {
            var injectComponentFromChild = Attribute.GetCustomAttribute(fieldInfo, typeof(InjectComponentFromChildAttribute)) as InjectComponentFromChildAttribute;
            if (ReferenceEquals(injectComponentFromChild, null))
                return;

            if (string.IsNullOrWhiteSpace(injectComponentFromChild.ChildName))
                throw new ArgumentException("Attribute value is empty", "childName");

            var injectedComponent = gameService.GetComponentInChildren(obj, injectComponentFromChild.ChildName, fieldInfo.FieldType);
            if (ReferenceEquals(injectedComponent, null))
                throw new MissingComponentException(string.Format("Could not find: {0}. Component {1} at object with name {0} is not added", injectComponentFromChild.ChildName, fieldInfo.FieldType));
        
            fieldInfo.SetValue(obj, injectedComponent);
        }

        public void InjectComponentFromObject(object obj, FieldInfo fieldInfo)
        {
            var injectComponentFromObject = Attribute.GetCustomAttribute(fieldInfo, typeof(InjectComponentFromObjectAttribute)) as InjectComponentFromObjectAttribute;
            if (ReferenceEquals(injectComponentFromObject, null))
                return;

            if (string.IsNullOrWhiteSpace(injectComponentFromObject.ObjectName))
                throw new ArgumentException("Attribute value is empty", "objectName");

            var componentName = injectComponentFromObject.ObjectName + fieldInfo.Name;
            if (objectPooling.Contains(componentName))
            {
                var injectedComponent = objectPooling.GetObject(componentName) as Component;
                fieldInfo.SetValue(obj, injectedComponent);
            }
            else
            {
                var injectedComponent = gameService.Find(injectComponentFromObject.ObjectName, fieldInfo.FieldType);
                if (ReferenceEquals(injectedComponent, null))
                    throw new NullReferenceException(string.Format("Could not find: {0}. Or component {1} at object with name {0} is not added", injectComponentFromObject.ObjectName, fieldInfo.FieldType));

                objectPooling.AddObject(componentName, injectedComponent);
                fieldInfo.SetValue(obj, injectedComponent);
            }
        }

        public void InjectAsSingle(object obj, FieldInfo fieldInfo)
        {
            var injectAsSingle = Attribute.GetCustomAttribute(fieldInfo, typeof(InjectAsSingleAttribute)) as InjectAsSingleAttribute;
            if (ReferenceEquals(injectAsSingle, null))
                return;

            var injectedObj = container.CreateObjectAsSingle(fieldInfo.FieldType);
            fieldInfo.SetValue(obj, injectedObj);
        }

        public void InjectAsTransient(object obj, FieldInfo fieldInfo)
        {
            var injectAsTransient = Attribute.GetCustomAttribute(fieldInfo, typeof(InjectAsTransientAttribute)) as InjectAsTransientAttribute;
            if (ReferenceEquals(injectAsTransient, null))
                return;

            var injectedObj = container.CreateObjectAsTransient(fieldInfo.FieldType);
            fieldInfo.SetValue(obj, injectedObj);
        }

        public void InjectFactory(object obj, FieldInfo fieldInfo)
        {
            var injectFactory = Attribute.GetCustomAttribute(fieldInfo, typeof(InjectFactoryAttribute)) as InjectFactoryAttribute;
            if (ReferenceEquals(injectFactory, null))
                return;

            if (!(fieldInfo.FieldType != typeof(IFactory)))
                throw new ArgumentException("FieldType is not factory");

            var argsType = fieldInfo.FieldType.BaseType.GenericTypeArguments;
            var objectActivator = container.CreateActivator(argsType.Last(), argsType.Take(argsType.Length - 1).ToArray());

            var injectedFactory = container.CreateObjectAsSingle(fieldInfo.FieldType, objectActivator);
            fieldInfo.SetValue(obj, injectedFactory);
        }
    }
}