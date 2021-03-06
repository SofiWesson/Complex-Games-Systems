using UnityEngine;
using UnityEditor;

namespace EasyResourceManager
{
    [CustomEditor(typeof(CollectionMethodManager))]
    public class CollectionMethodEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            CollectionMethodManager manager = (CollectionMethodManager)target;

            if (GUILayout.Button("Add Attribute"))
            {
                manager.AddAttributes();
            }

            if (GUILayout.Button("Add Collection Method"))
            {
                manager.AddCollectionMethod();
            }

            if (GUILayout.Button("Edit Attribute"))
            {
                manager.EditAttribute();
            }

            if (GUILayout.Button("Edit Collection Method"))
            {
                manager.EditCollectionMethod();
            }

            if (GUILayout.Button("Remove Attribute"))
            {
                manager.RemoveAttribute();
            }

            if (GUILayout.Button("Remove Collection Method"))
            {
                manager.RemoveCollectionMethod();
            }

            if (GUILayout.Button("Reload Lists"))
            {
                manager.ReloadLists();
            }

            if (GUILayout.Button("Clear All"))
            {
                manager.ClearAll();
            }
        }
    }
}