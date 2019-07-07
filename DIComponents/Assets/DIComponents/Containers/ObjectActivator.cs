using System;
using System.Linq.Expressions;

namespace DIComponents.Core
{
    public class ObjectActivator<T> : IObjectActivator
    {
        private delegate TValue Activator<TValue>(params object[] args);

        private Activator<T> activator;

        public ObjectActivator(params Type[] argsType)
        {
            var type = typeof(T);
            var ctor = type.GetConstructor(argsType);
            var paramsInfo = ctor.GetParameters();
            var param = Expression.Parameter(typeof(object[]), "args");
            var argsExpression = new Expression[paramsInfo.Length];
            for (int i = 0; i < paramsInfo.Length; i++)
            {
                var index = Expression.Constant(i);
                var paramType = paramsInfo[i].ParameterType;
                var paramAccessorExp = Expression.ArrayIndex(param, index);
                var paramCastExp = Expression.Convert(paramAccessorExp, paramType);
                argsExpression[i] = paramCastExp;
            }
            var newExp = Expression.New(ctor, argsExpression);
            LambdaExpression lambda = Expression.Lambda(typeof(Activator<T>), newExp, param);
            activator = (Activator<T>)lambda.Compile();
        }

        public object Create(params object[] args)
        {
            return activator.Invoke(args);
        }
    }
}
