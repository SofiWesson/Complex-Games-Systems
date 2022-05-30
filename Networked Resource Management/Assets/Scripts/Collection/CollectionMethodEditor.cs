using UnityEngine;
using UnityEditor;

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
            manager.AddResource();
        }

        if (GUILayout.Button("Edit Attribute"))
        {
            manager.EditAttribute();
        }

        if (GUILayout.Button("Edit Collection Method"))
        {
            manager.EditResource();
        }

        if (GUILayout.Button("Remove Attribute"))
        {
            manager.RemoveAttribute();
        }

        if (GUILayout.Button("Remove Collection Method"))
        {
            manager.RemoveResource();
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
