using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DIComponents
{
    public class DIContainer
    {
        private Dictionary<int, object> container = new Dictionary<int, object>();
        private Dictionary<int, Func<object>> objectActivators = new Dictionary<int, Func<object>>(); 

        public object CreateObjectAsSingle(Type type)
        {
            var hash = type.GetHashCode();
            if (container.ContainsKey(hash))
                return container[hash];

            Func<object> activator = null;
            if (!objectActivators.ContainsKey(hash))
            {
                activator = CreateActivator(type);
                objectActivators.Add(hash, activator);
            }
            else
                activator = objectActivators[hash];

            var obj = activator.Invoke();
            container.Add(hash, obj);
            return obj;
        }

        public object CreateObjectAsTransient(Type type)
        {
            int hash = type.GetHashCode();
            if (objectActivators.ContainsKey(hash))
                return objectActivators[hash].Invoke();

            var activator = CreateActivator(type);
            objectActivators.Add(hash, activator);
            return activator.Invoke();
        }

        public T CreateObjectAsSingle<T>() where T : class
        {
            var type = typeof(T);
            return CreateObjectAsSingle(type) as T;
        }

        public T CreateObjectAsTransient<T>() where T : class
        {
            var type = typeof(T);
            return CreateObjectAsTransient(type) as T;
        }

        private Func<object> CreateActivator(Type type)
        {
            var ctor = type.GetConstructor(Type.EmptyTypes);
            NewExpression newExp = Expression.New(ctor);
            LambdaExpression lambda = Expression.Lambda(typeof(Func<object>), newExp, new ParameterExpression[0]);
            return (Func<object>)lambda.Compile();
        }
    }
}