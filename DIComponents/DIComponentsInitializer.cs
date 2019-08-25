using System.Reflection;
using UnityEngine;
using DIComponents.Core;

namespace DIComponents
{
    public class DIComponentsInitializer
    {
        #if UNITY_EDITOR
        private static IInjector componentsInjector = new DebugInjector(new UnityGameService());
        #else
        private static IInjector componentsInjector = new Injector(new UnityGameService());        
        #endif

        public static void Inject(object obj)
        {
            var type = obj.GetType();
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var field in fields)
            {
                componentsInjector.InjectComponent(obj, field);
                componentsInjector.InjectComponentFromChild(obj, field);
                componentsInjector.InjectComponentFromObject(obj, field);
                componentsInjector.InjectAsSingle(obj, field);
                componentsInjector.InjectAsTransient(obj, field);
                componentsInjector.InjectFactory(obj, field);
            }
        }

        public static void Inject(GameObject go)
        {
            var components = go.GetComponents(typeof(Component));
            foreach (var component in components)
                Inject(component);
        }

        public static void InjectWithChildrens(GameObject go)
        {
            var childrens = go.GetComponentsInChildren<Transform>();
            foreach (var child in childrens)
                if (!ReferenceEquals(child, null) && !ReferenceEquals(child.gameObject, null) && !ReferenceEquals(child.gameObject, go))
                    InjectWithChildrens(child.gameObject);
            Inject(go);
        }
    }
}