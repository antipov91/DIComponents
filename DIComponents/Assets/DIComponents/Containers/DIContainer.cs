using System;
using System.Collections.Generic;

namespace DIComponents.Core
{
    public class DIContainer
    {
        private Dictionary<string, object> container = new Dictionary<string, object>();
        private Dictionary<string, IObjectActivator> objectActivators = new Dictionary<string, IObjectActivator>(); 

        public object CreateObjectAsSingle(Type type, params object[] args)
        {
            var key = type.Name;
            if (container.ContainsKey(key))
                return container[key];

            IObjectActivator activator = null;
            if (!objectActivators.ContainsKey(key))
            {
                var argsType = Array.ConvertAll(args, x => x.GetType());
                activator = CreateActivator(type, argsType);
                objectActivators.Add(key, activator);
            }
            else
                activator = objectActivators[key];

            var obj = activator.Create(args);
            container.Add(key, obj);

            return obj;
        }

        public object CreateObjectAsTransient(Type type, params object[] args)
        {
            var key = type.Name;
            if (objectActivators.ContainsKey(key))
                return objectActivators[key].Create(args);

            var argsType = Array.ConvertAll(args, x => x.GetType());
            var activator = CreateActivator(type, argsType);
            objectActivators.Add(key, activator);
            return activator.Create(args);
        }

        public T CreateObjectAsSingle<T>(params object[] args) where T : class
        {
            var type = typeof(T);
            return CreateObjectAsSingle(type, args) as T;
        }

        public T CreateObjectAsTransient<T>(params object[] args) where T : class
        {
            var type = typeof(T);
            return CreateObjectAsTransient(type, args) as T;
        }

        public IObjectActivator CreateActivator(Type type, params Type[] argsType)
        {
            var genericClass = typeof(ObjectActivator<>);
            var construcredClass = genericClass.MakeGenericType(type);
            return Activator.CreateInstance(construcredClass, argsType) as IObjectActivator;
        }
    }
}