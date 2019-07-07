using System.Reflection;

namespace DIComponents.Core
{
    public interface IInjector
    {
        void InjectComponent(object obj, FieldInfo fieldInfo);
        void InjectComponentFromChild(object obj, FieldInfo fieldInfo);
        void InjectComponentFromObject(object obj, FieldInfo fieldInfo);
        void InjectAsSingle(object obj, FieldInfo fieldInfo);
        void InjectAsTransient(object obj, FieldInfo fieldInfo);
        void InjectFactory(object obj, FieldInfo fieldInfo);
    }
}