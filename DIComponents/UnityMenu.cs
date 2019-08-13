using UnityEditor;
using UnityEngine;

namespace DIComponents.Core
{
    public class UnityMenu : MonoBehaviour
    {
        [MenuItem("DIComponents/Create ComponentsInitializer")]
        static void CreateComponentsInitializer()
        {
            var go = new GameObject("ComponentsInitializer");
            go.AddComponent<MonoDIComponentsInitializer>();
            Undo.RegisterCreatedObjectUndo(go, string.Format("Create {0}", go.name));
            Selection.activeObject = go;
        }
    }
}
