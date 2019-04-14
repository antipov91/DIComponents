﻿using System.Reflection;
using UnityEngine;

namespace DIComponents
{
    public class DIComponentsInitializer
    {
        #if UNITY_EDITOR
        private static IComponentsInjector componentsInjector = new DebugComponentsInjector();
        #else
        private static IComponentsInjector componentsInjector = new ComponentsInjector();        
        #endif

        public static void Inject(GameObject go)
        {
            var components = go.GetComponents(typeof(Component));
            foreach (var component in components)
            {
                var type = component.GetType();
                var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                foreach (var field in fields)
                {
                    componentsInjector.InjectComponent(component, field);
                    componentsInjector.InjectComponentFromChild(component, field);
                    componentsInjector.InjectComponentFromObject(component, field);
                }
            }
        }
    }
}