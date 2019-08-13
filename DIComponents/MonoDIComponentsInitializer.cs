using UnityEngine;

namespace DIComponents
{
    public class MonoDIComponentsInitializer : MonoBehaviour
    {
        private void OnEnable()
        {
            var allObjects = FindObjectsOfType<GameObject>();
            foreach (var go in allObjects)
                DIComponentsInitializer.Inject(go);
        }
    }
}