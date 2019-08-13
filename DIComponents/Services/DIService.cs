using UnityEngine;

namespace DIComponents
{
    public static class DIService
    {
        public static GameObject InstantiateAndInject(Object original)
        {
            var go = Object.Instantiate(original) as GameObject;
            DIComponentsInitializer.InjectWithChildrens(go);
            return go;
        }

        public static GameObject InstantiateAndInject(Object original, Transform parent)
        {
            var go = Object.Instantiate(original, parent) as GameObject;
            DIComponentsInitializer.InjectWithChildrens(go);
            return go;
        }

        public static GameObject InstantiateAndInject(Object original, Transform parent, bool instantiateInWorldSpace)
        {
            var go = Object.Instantiate(original, parent, instantiateInWorldSpace) as GameObject;
            DIComponentsInitializer.InjectWithChildrens(go);
            return go;
        }

        public static GameObject InstantiateAndInject(Object original, Vector3 position, Quaternion rotation)
        {
            var go = Object.Instantiate(original, position, rotation) as GameObject;
            DIComponentsInitializer.InjectWithChildrens(go);
            return go;
        }

        public static GameObject InstantiateAndInject(Object original, Vector3 position, Quaternion rotation, Transform parent)
        {
            var go = Object.Instantiate(original, position, rotation, parent) as GameObject;
            DIComponentsInitializer.InjectWithChildrens(go);
            return go;
        }

        public static T InstantiateAndInject<T>(T original) where T : Object
        {
            var go = Object.Instantiate(original) as GameObject;
            DIComponentsInitializer.InjectWithChildrens(go);
            return go as T;
        }

        public static T InstantiateAndInject<T>(T original, Transform parent) where T : Object
        {
            var go = Object.Instantiate(original, parent) as GameObject;
            DIComponentsInitializer.InjectWithChildrens(go);
            return go as T;
        }

        public static T InstantiateAndInject<T>(T original, Transform parent, bool instantiateInWorldSpace) where T : Object
        {
            var go = Object.Instantiate(original, parent, instantiateInWorldSpace) as GameObject;
            DIComponentsInitializer.InjectWithChildrens(go);
            return go as T;
        }

        public static T InstantiateAndInject<T>(T original, Vector3 position, Quaternion rotation) where T : Object
        {
            var go = Object.Instantiate(original, position, rotation) as GameObject;
            DIComponentsInitializer.InjectWithChildrens(go);
            return go as T;
        }

        public static T InstantiateAndInject<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : Object
        {
            var go = Object.Instantiate(original, position, rotation, parent) as GameObject;
            DIComponentsInitializer.InjectWithChildrens(go);
            return go as T;
        }
    }
}